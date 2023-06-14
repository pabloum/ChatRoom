using System;
namespace Web.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        string GetClaim(string claimName);
        bool IsUserLogged();
        string GetSessionString(string key);
        void SetSessionString(string key, string value);
    }
}

