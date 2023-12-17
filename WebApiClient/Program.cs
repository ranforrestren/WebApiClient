using System.Diagnostics;
using System.Net.Http.Headers;


namespace WebApiClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            await ProcessRepositoriesAsync(client);

            static async Task ProcessRepositoriesAsync(HttpClient client)
            {
                var json = await client.GetStringAsync(
                    "https://api.github.com/orgs/dotnet/repos");

                Console.Write(json);
            }
        }
    }
}
