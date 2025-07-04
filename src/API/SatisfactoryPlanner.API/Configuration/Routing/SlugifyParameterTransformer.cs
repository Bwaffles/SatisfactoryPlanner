﻿using System.Text.RegularExpressions;

namespace SatisfactoryPlanner.API.Configuration.Routing
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            return value == null
                ? null
                : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}