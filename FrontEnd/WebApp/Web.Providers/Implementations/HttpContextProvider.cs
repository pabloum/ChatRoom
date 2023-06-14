using System;
using Entities;
using Microsoft.AspNetCore.Http;
using Web.Providers.Contracts;

namespace Web.Providers.Implementations
{
    public class HttpContextProvider : IHttpContextProvider
    {
        private IHttpContextAccessor _httpContextAccessor;

        public HttpContextProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetClaim(string claimName)
		{
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null && httpContext.User.Identity.IsAuthenticated)
            {
                string claim = httpContext.User.FindFirst(claimName).Value;

                return claim;
            }
            return String.Empty;
        }

        public bool IsUserLogged()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetSessionString(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);
        }

        public void SetSessionString(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);
        }
    }
}

