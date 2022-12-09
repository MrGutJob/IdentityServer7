// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using IdentityModel;

#pragma warning disable 1591

namespace IdentityServer7.Stores.Stores.Serialization;

public class ClaimsPrincipalConverter : JsonConverter<ClaimsPrincipal>
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(ClaimsPrincipal) == objectType;
    }

    public override ClaimsPrincipal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var source = JsonSerializer.Deserialize<ClaimsPrincipalLite>(ref reader,options);
        if (source == null) return null;

        var claims = source.Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType));
        var id = new ClaimsIdentity(claims, source.AuthenticationType, JwtClaimTypes.Name, JwtClaimTypes.Role);
        var target = new ClaimsPrincipal(id);
        return target;
    }

    public override void Write(Utf8JsonWriter writer, ClaimsPrincipal value, JsonSerializerOptions options)
    {
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        var source = value;

        var target = new ClaimsPrincipalLite
        {
            AuthenticationType = source.Identity?.AuthenticationType ?? throw new NullReferenceException(),
            Claims = source.Claims.Select(x => new ClaimLite { Type = x.Type, Value = x.Value, ValueType = x.ValueType }).ToArray()
        };
        JsonSerializer.Serialize(writer, target,options);
    }

    //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //{
    //    var source = serializer.Deserialize<ClaimsPrincipalLite>(reader);
    //    if (source == null) return null;

    //    var claims = source.Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType));
    //    var id = new ClaimsIdentity(claims, source.AuthenticationType, JwtClaimTypes.Name, JwtClaimTypes.Role);
    //    var target = new ClaimsPrincipal(id);
    //    return target;
    //}

    //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //{
    //    var source = (ClaimsPrincipal)value;

    //    var target = new ClaimsPrincipalLite
    //    {
    //        AuthenticationType = source.Identity.AuthenticationType,
    //        Claims = source.Claims.Select(x => new ClaimLite { Type = x.Type, Value = x.Value, ValueType = x.ValueType }).ToArray()
    //    };
    //    serializer.Serialize(writer, target);
    //}
}