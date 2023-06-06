using System;
using Entities;

namespace Security
{
	public interface IAuthentication
	{
		bool CheckCredentials(Credentials credentials);
        void RegisterNewUser(Credentials credentials);
    }


    public class Authentication : IAuthentication
	{
        private IEnumerable<Credentials> _credentials; // temporal. To be replaced with BEs tables

        public Authentication()
        {
            _credentials = new List<Credentials>
            {
                new Credentials { UserName = "admin", Password = "123"},
                new Credentials { UserName = "pum", Password = "pass"},
                new Credentials { UserName = "jbs", Password = "pass2"},
            };
        }

        public bool CheckCredentials(Credentials credentials)
        {
            return _credentials.Where(c => c.UserName == credentials.UserName && c.Password == credentials.Password).Any();
        }

        public void RegisterNewUser(Credentials credentials)
        {
            if (!_credentials.Where(c => c.UserName == credentials.UserName).Any())
            {
                _credentials = _credentials.Append(credentials);
            }
        }
    }
}

