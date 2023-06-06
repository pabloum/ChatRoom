using Entities;

namespace Security
{
    public interface IAuthentication
	{
        Task<bool> CheckCredentials(Credentials credentials);
        Task RegisterNewUser(User credentials);
    }
}

