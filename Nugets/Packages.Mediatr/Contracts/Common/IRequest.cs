﻿namespace Packages.Mediatr.Contracts.Common;

public interface IRequest;

public interface IRequest<TResponse>
    : IRequest
    where TResponse : class;
