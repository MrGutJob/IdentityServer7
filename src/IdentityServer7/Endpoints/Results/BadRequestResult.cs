﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer7.Hosting;
using Microsoft.AspNetCore.Http;
using IdentityServer7.Extensions;

namespace IdentityServer7.Endpoints.Results
{
    public class BadRequestResult : IEndpointResult
    {
        public string Error { get; set; }
        public string ErrorDescription { get; set; }

        public BadRequestResult(string error = null, string errorDescription = null)
        {
            Error = error;
            ErrorDescription = errorDescription;
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.SetNoCache();

            if (Error.IsPresent())
            {
                var dto = new ResultDto
                {
                    error = Error,
                    error_description = ErrorDescription
                };

                await context.Response.WriteJsonAsync(dto);
            }
        }

        public class ResultDto
        {
            public string error { get; set; }
            public string error_description { get; set; }
        }
    }
}