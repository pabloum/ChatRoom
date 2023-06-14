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
            if (_httpContextProvider.IsUserLogged())
            {
                await GenerateToken();
            }

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        public async Task<T> Post<T>(string url, string payload)
        {
            if (_httpContextProvider.IsUserLogged())
            {
                await GenerateToken();
            }

            var response = await client.PostAsync(url, CreateContent(payload));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        private async Task GenerateToken()
        {
            string bearerToken = _httpContextProvider.GetSessionString("token");
           
            if (String.IsNullOrEmpty(bearerToken) || DateTime.Parse(_httpContextProvider.GetSessionString("expires_at")) < DateTime.Now)
            {
                string username = _httpContextProvider.GetClaim("Username");

                var payload = JsonSerializer.Serialize(new
                {
                    username = _httpContextProvider.GetClaim("Username"),
                    password = _httpContextProvider.GetSessionString("pass")
                });

                var response = await client.PostAsync("api/Authentication", CreateContent(payload));

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsAsync<TokenStructure>();
                    bearerToken = token.access_token;
                    _httpContextProvider.SetSessionString("access_token", bearerToken);
                    _httpContextProvider.SetSessionString("expires_at", token.expires_at);
                }
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
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

