using System.Text.Json;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    public static class HttpResponseMessageExtensions
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage message)
        {
            var content = await message.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, Options)!;
        }
    }
}