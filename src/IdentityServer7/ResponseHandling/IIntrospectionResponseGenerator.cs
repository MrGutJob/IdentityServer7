﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer7.Validation;

namespace IdentityServer7.ResponseHandling
{
    /// <summary>
    /// Interface for introspection response generator
    /// </summary>
    public interface IIntrospectionResponseGenerator
    {
        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
        Task<Dictionary<string, object>> ProcessAsync(IntrospectionRequestValidationResult validationResult);
    }
}