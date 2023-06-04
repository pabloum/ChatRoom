using System;
using Web.Providers.Contracts;
using Web.Providers.Entities;

namespace Web.Providers.Implementations
{
	public class MessageProvider : IMessageProvider
	{
        private ServiceHandler _serviceHandler;

        public MessageProvider()
		{
            _serviceHandler = new ServiceHandler(); // TODO: Use DI
        }

        public Task<Message> CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetAllMessagedByRoom(int roomId)
        {
            var response = await _serviceHandler.Get<IEnumerable<Message>>($"api/Message/{roomId}");
            return response != null ? response : new List<Message>();
        }
    }
}

