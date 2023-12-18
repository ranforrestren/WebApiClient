using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;


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

            var repositories = await ProcessRepositoriesAsync(client);

            foreach (var repo in repositories)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Homepage: {repo.Homepage}");
                Console.WriteLine($"Github: {repo.GitHubHomeUrl}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Last push: {repo.LastPush}");
                Console.WriteLine();
            }
        }

        static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
        {
            await using Stream stream =
                await client.GetStreamAsync("http://api.github.com/orgs/dotnet/repos");
            var repositories =
                await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
            return repositories ?? new();
        }
    }
}
