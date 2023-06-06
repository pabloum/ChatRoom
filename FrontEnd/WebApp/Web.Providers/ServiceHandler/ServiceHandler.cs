using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Entities;
using Microsoft.AspNetCore.Http;

namespace Web.Providers
{
    public class ServiceHandler : IServiceHandler
    {
        private HttpClient client;
        private IHttpContextAccessor _httpContextAccessor;

        public ServiceHandler(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                string username = httpContext.User.FindFirst("Username").Value;
                //string role = httpContext.User.FindFirst(ClaimTypes.Role).Value;

                object hardcodedPayload = new //todo: take this from cookie
                {
                    id = 1,
                    name = "Pablo Uribe",
                    username = username,
                    password = "123"
                };

                HttpContent content = new StringContent(JsonSerializer.Serialize(hardcodedPayload), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Authentication", content);

                if (response.IsSuccessStatusCode)
                {
                    return (await response.Content.ReadAsAsync<TokenStructure>()).access_token;
                }
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

