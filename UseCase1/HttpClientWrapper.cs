namespace UseCase1; 

public interface IHttpClientWrapper
{
  Task<HttpResponseMessage> GetAsync(string url);
}

public class HttpClientWrapper : IHttpClientWrapper
{
  private readonly HttpClient _client;

  public HttpClientWrapper(IHttpClientFactory clientFactory)
  {
    _client = clientFactory.CreateClient();
  }

  public Task<HttpResponseMessage> GetAsync(string url)
  {
    return _client.GetAsync(url);
  }
}