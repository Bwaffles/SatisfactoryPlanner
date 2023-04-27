using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Configuration.Extensions
{
    internal static class HttpRequestExtensions
    {
        public static async Task<object?> ReadAsJsonAsync(this HttpRequest request, Type type,
            JsonSerializerOptions? options = null)
        {
            request.Body.Position = 0;
            var result = await request.ReadFromJsonAsync(type, options);
            // reset the position again to let endpoint middleware read it
            request.Body.Position = 0;
            return result;
        }
    }
}