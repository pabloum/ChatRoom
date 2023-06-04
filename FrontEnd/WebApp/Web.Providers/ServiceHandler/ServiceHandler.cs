using System;
using System.Net.Http.Headers;

namespace Web.Providers
{
    public class ServiceHandler
    {
        private HttpClient client;

        public ServiceHandler()
        {
            client = new HttpClient(); //TODO: Use DI
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
    }
}

