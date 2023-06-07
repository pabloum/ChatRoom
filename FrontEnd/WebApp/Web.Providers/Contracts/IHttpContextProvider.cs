using System;
namespace Web.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        string GetClaim(string claimName);
        bool IsUserLogged();
    }
}

