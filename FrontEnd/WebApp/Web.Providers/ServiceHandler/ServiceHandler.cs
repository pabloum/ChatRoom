using System;
using System.Net.Http.Headers;
using System.Text;

namespace Web.Providers
{
    public interface IServiceHandler
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, string payload);
    }

    public class ServiceHandler : IServiceHandler
    {
        private HttpClient client;

        public ServiceHandler(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri("https://localhost:2701/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Get<T>(string url)
        {
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        public async Task<T> Post<T>(string url, string payload)
        {
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }
    }
}

