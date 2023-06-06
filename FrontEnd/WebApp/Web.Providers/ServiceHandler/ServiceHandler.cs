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
        }

        public async Task<T> Get<T>(string url)
        {
            await GenerateToken();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        public async Task<T> Post<T>(string url, string payload)
        {
            await GenerateToken();
            var response = await client.PostAsync(url, CreateContent(payload));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        private async Task GenerateToken()
        {
            string username = _httpContextProvider.GetClaim("Username");

            var payload = JsonSerializer.Serialize(new
            {
                username = _httpContextProvider.GetClaim("Username"),
                password = "123" //TODO: retrieve this  from a save place
            });

            var response = await client.PostAsync("api/Authentication", CreateContent(payload));

            if (response.IsSuccessStatusCode)
            {
                var bearerToken = (await response.Content.ReadAsAsync<TokenStructure>()).access_token;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }
        }

        private StringContent? CreateContent(string payload)
        {
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }
    }

    public class TokenStructure
    {
        public string access_token { get; set; }
        public string expires_at { get; set; }
    }
}

