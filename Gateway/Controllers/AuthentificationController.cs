namespace GatewayApi.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

[ApiController]
[Route("api/v1/identity/[controller]")]
public class AuthentificationController : ControllerBase
{
    [HttpGet("main-page")]
    public ActionResult Mainpage()
    {
        return this.RedirectToAction("login");
    }

    [HttpGet("login")]
    public ActionResult LogIn(string returnUrl)
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = this.Url.IsLocalUrl(returnUrl) ? returnUrl : "/main-page",
        };

        return this.Challenge(properties, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> LogOut(string returnUrl)
    {
        var result = await this.HttpContext.AuthenticateAsync();
        if (result is not { Succeeded: true })
        {
            // Only allow local return URLs to prevent open redirect attacks.
            return this.Redirect(this.Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        await this.HttpContext.SignOutAsync();

        var properties = new AuthenticationProperties(new Dictionary<string, string?>
        {
            [OpenIddictClientAspNetCoreConstants.Properties.IdentityTokenHint] =
                result.Properties.GetTokenValue(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelIdentityToken),
        })
        {
            RedirectUri = this.Url.IsLocalUrl(returnUrl) ? returnUrl : "/",
        };

        return this.SignOut(properties, OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpGet("~/callback/login/{provider}")]
    [HttpPost("~/callback/login/{provider}")]
    [IgnoreAntiforgeryToken]
    public async Task<ActionResult> LogInCallback()
    {
        var result = await this.HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
        if (result.Principal is not ClaimsPrincipal { Identity.IsAuthenticated: true })
        {
            throw new InvalidOperationException("The external authorization data cannot be used for authentication.");
        }

        var identity = new ClaimsIdentity(
            authenticationType: "ExternalLogin",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        identity.SetClaim(ClaimTypes.Email, result.Principal.GetClaim(ClaimTypes.Email))
                .SetClaim(ClaimTypes.Name, result.Principal.GetClaim(ClaimTypes.Name))
                .SetClaim(ClaimTypes.NameIdentifier, result.Principal.GetClaim(ClaimTypes.NameIdentifier));

        identity.SetClaim(Claims.Private.RegistrationId, result.Principal.GetClaim(Claims.Private.RegistrationId))
                .SetClaim(Claims.Private.ProviderName, result.Principal.GetClaim(Claims.Private.ProviderName));

        var properties = new AuthenticationProperties(items: result?.Properties?.Items!)
        {
            RedirectUri = result?.Properties?.RedirectUri ?? "/",
        };

        properties.StoreTokens(result?.Properties?.GetTokens()?.Where(token => token.Name is
            OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken or
            OpenIddictClientAspNetCoreConstants.Tokens.BackchannelIdentityToken or
            OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken)!);

        return this.SignIn(new ClaimsPrincipal(identity), properties);
    }

    [HttpGet("~/callback/logout/{provider}")]
    [HttpPost("~/callback/logout/{provider}")]
    [IgnoreAntiforgeryToken]
    public async Task<ActionResult> LogOutCallback()
    {
        var result = await this.HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);
        return this.Redirect(result!.Properties!.RedirectUri!);
    }
}
