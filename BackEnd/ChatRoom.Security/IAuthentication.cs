using ChatRoom.Entities.Domain;
using ChatRoom.Services.Base;

namespace ChatRoom.Security
{
    public interface IAuthentication
	{
        object GetToken(Credentials credentials);
    }
}

