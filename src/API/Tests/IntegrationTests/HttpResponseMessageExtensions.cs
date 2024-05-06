using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage message)
            => (await message.Content.ReadFromJsonAsync<T>())!;
    }
}