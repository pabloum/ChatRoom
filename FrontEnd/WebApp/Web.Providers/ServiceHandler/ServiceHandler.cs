using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Http;
using Web.Providers.Contracts;

namespace Web.Providers
{
    public class ServiceHandler : IServiceHandler
    {
        private HttpClient client;
        private IHttpContextProvider _httpContextProvider;

        public ServiceHandler(HttpClient httpClient, IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider;
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
            string username = _httpContextProvider.GetClaim("Username");

            var payload = new
            {
                username = _httpContextProvider.GetClaim("Username"),
                password = "123" //TODO: retrieve this  from a save place
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
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

