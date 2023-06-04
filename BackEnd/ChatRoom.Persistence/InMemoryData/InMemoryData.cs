using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.InMemoryData;

namespace ChatRoom.Persistence.InMemoryData
{
    public class InMemoryData : IInMemoryData
    {
        private List<Room> _inMemoryRooms;
        private List<Stock> _inMemoryStocks;
        private List<User> _inMemoryUsers;
        private List<Message> _inMemoryMessages;

        public InMemoryData()
        {
            _inMemoryRooms = new List<Room>
            {
                new Room { RoomId = 1, RoomName = "General Knowledge"},
                new Room { RoomId = 2, RoomName = "Sports"},
                new Room { RoomId = 3, RoomName = "Literature"},
                new Room { RoomId = 4, RoomName = "Programming"},
                new Room { RoomId = 4, RoomName = "Physics"},
            };

            _inMemoryStocks = new List<Stock>
            {
                new Stock
                {
                    Code = "aapl.us",
                    DateTime = DateTime.Now,
                    Open = (decimal)181.03,
                    High = (decimal)181.78,
                    Low = (decimal)179.26,
                    Close = (decimal)180.93,
                    Volume = 39457561,
                },
            };

            _inMemoryUsers = new List<User>
            {
                new User { UserId = 1, Name = "Pablo Uribe", Username = "puribe" },
            };

            _inMemoryMessages = new List<Message>
            {
            };
        }

        public Room CreateRoom(Room room)
        {
            room.RoomId = _inMemoryRooms.Max(r => r.RoomId) + 1;
            _inMemoryRooms.Add(room);
            return room;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _inMemoryRooms;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _inMemoryStocks;
        }

        public Stock GetStockByStockCode(string stockCode)
        {
            return _inMemoryStocks.FirstOrDefault(s => s.Code == stockCode);
        }

        public Message CreateMessage(Message message)
        {
            message.RoomId = _inMemoryMessages.Max(r => r.MessageId) + 1;
            _inMemoryMessages.Add(message);
            return message;
        }

        public IEnumerable<Message> GetMessegesByRoom(int roomId)
        {
            return _inMemoryMessages.Where(m => m.RoomId == roomId);
        }

        public User CreateUser(User user)
        {
            user.UserId = _inMemoryUsers.Max(r => r.UserId) + 1;
            _inMemoryUsers.Add(user);
            return user;
        }

        public User GetUserById(int userId)
        {
            return _inMemoryUsers.FirstOrDefault(u => u.UserId == userId);
        }
    }
}

