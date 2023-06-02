using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.InMemoryData;

namespace ChatRoom.Persistence.InMemoryData
{
    public class InMemoryData : IInMemoryData
    {
        private List<Room> _inMemoryRoom;

        public InMemoryData()
        {
            _inMemoryRoom = new List<Room>
            {
                new Room { RoomId = 1, RoomName = "General Knowledge"},
                new Room { RoomId = 2, RoomName = "Sports"},
                new Room { RoomId = 3, RoomName = "Literature"},
                new Room { RoomId = 4, RoomName = "Programming"},
            };
        }

        public IEnumerable<Room> GetAll()
        {
            return _inMemoryRoom;
        }
    }
}

