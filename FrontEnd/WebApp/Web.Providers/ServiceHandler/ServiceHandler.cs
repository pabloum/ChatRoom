using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Http;
using Web.Providers.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Web.Providers
{
    public class ServiceHandler : IServiceHandler
    {
        private HttpClient client;
        private readonly IHttpContextProvider _httpContextProvider;

        public ServiceHandler(IConfiguration configuration, HttpClient httpClient, IHttpContextProvider httpContextProvider)
        {
            var backendUrl = configuration.GetSection("BackendBaseUrl").Value;
            _httpContextProvider = httpContextProvider;
            client = httpClient;
            client.BaseAddress = new Uri(backendUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            var token = new TokenStructure
            {
                access_token = _httpContextProvider.GetSessionString("token"),
                expires_at = _httpContextProvider.GetSessionString("expires_at")
            };

            if (_httpContextProvider.IsUserLogged())
            {
                var isTokenInvalid = String.IsNullOrEmpty(token.access_token)
                             || DateTime.Parse(token.expires_at) < DateTime.Now;

                token = isTokenInvalid ? await GetToken() : token;

                _httpContextProvider.SetSessionString("access_token", token.access_token);
                _httpContextProvider.SetSessionString("expires_at", token.expires_at);
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }

        private async Task<TokenStructure> GetToken()
        {
            string username = _httpContextProvider.GetClaim("Username");

            var payload = JsonSerializer.Serialize(new
            {
                username = _httpContextProvider.GetClaim("Username"),
                password = _httpContextProvider.GetClaim("pass")
            });

            var response = await client.PostAsync("api/Authentication", CreateContent(payload));
            return await response.Content.ReadAsAsync<TokenStructure>();
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

