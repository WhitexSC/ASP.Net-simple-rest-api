using System.Net.Http.Headers;
using System.Text.Json;

namespace TestAPI;

public class GitHubService
{
  public HttpClient CreateHttpClient()
  {
    var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
      new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
    return client;
  }

  public async Task<List<Repository>> FetchRepositoriesAsync(HttpClient client)
  {
    await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
    return repositories ?? new();
  }
}