using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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
            var bearerToken = await GenerateToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        public async Task<T> Post<T>(string url, string payload)
        {
            var bearerToken = await GenerateToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        private async Task<string> GenerateToken()
        {
            object hardcodedPayload = new //TODO update this from the BE to admit other users 
            {
                userName = "puribe",
                password = "123"
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(hardcodedPayload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Authentication", content);

            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync<TokenStructure>()).access_token;
            }

            return String.Empty;
        }
    }

    public class TokenStructure
    {
        public string access_token { get; set; }
        public string expires_at { get; set; }
    }
}

