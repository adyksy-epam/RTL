using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RTL.TvMazeScraper.Scraper.App.Settings;

namespace RTL.TvMazeScraper.Scraper.App.Http
{
    public class RateLimitedHttpClient : IHttpClient
    {
        private readonly IRestClient _client;
        private readonly HttpSettings _settings;

        public RateLimitedHttpClient(HttpSettings settings, IRestClient client)
        {
            _settings = settings;
            _client = client;
        }

        public async Task<string> GetAsync(string requestUri)
        {
            while (true)
            {
                _client.BaseUrl = new Uri(requestUri, UriKind.Absolute);
                var response = await _client.ExecuteTaskAsync(new RestRequest(string.Empty, Method.GET));

                if (response.IsSuccessful)
                {
                    return response.Content;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return string.Empty;
                }
                else if (response.StatusCode != HttpStatusCode.TooManyRequests)
                {
                    throw new Exception();
                }

                await Task.Delay(TimeSpan.FromSeconds(_settings.RequestRetryDelay));
            }
        }
    }
}
