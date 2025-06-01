namespace GatewayApi.Controllers;

using GatewayApi.Configurations.Settings.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/identity/[controller]")]
public class AccountController(IdentityProviderSettings identityProviderSettings) : ControllerBase
{
    [HttpGet("Register")]
    public ActionResult Register(string returnUrl)
        => this.Redirect(
            string.Format(
                "{0}/{1}?clientId={2}&returnUrl={3}",
                identityProviderSettings.Uri,
                "account/register",
                identityProviderSettings.ClientId,
                returnUrl));
}
