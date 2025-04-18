﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace IdentityServer7.Validation
{
    /// <summary>
    /// Handles validation of resource owner password credentials
    /// </summary>
    public interface IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        Task ValidateAsync(ResourceOwnerPasswordValidationContext context);
    }
}