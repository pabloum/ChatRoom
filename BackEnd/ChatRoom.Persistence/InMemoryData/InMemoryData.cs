using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.InMemoryData;

namespace ChatRoom.Persistence.InMemoryData
{
    public class InMemoryData : IInMemoryData
    {
        private List<Room> _inMemoryRooms;
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
                new Room { RoomId = 5, RoomName = "Physics"},
            };

            _inMemoryUsers = new List<User>
            {
                new User { UserId = 1, Name = "Pablo Uribe", Username = "puribe", Password = "123"},
                new User { UserId = 2, Name = "Mr Evaluator", Username = "evaluator", Password = "123" },
                new User { UserId = 3, Name = "Leo Messi", Username = "lmessi", Password = "123" },
                new User { UserId = 4, Name = "Rafa Nadal", Username = "rnadal", Password = "123" },
                new User { UserId = 5, Name = "Bot", Username = "TheStockBot", Password = "123"}
            };

            _inMemoryMessages = new List<Message>
            {
                new Message
                {
                    MessageId = 1,
                    MessagePrompt = "Hello World!!",
                    RoomId = 1,
                    Room = _inMemoryRooms.FirstOrDefault(r => r.RoomId == 1),
                    PostingTime = DateTime.Now,
                    UserId = 1,
                    User = _inMemoryUsers.FirstOrDefault(u => u.UserId == 1)
                }
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

        public Message CreateMessage(Message message)
        {
            message.MessageId = _inMemoryMessages.Max(r => r.MessageId) + 1;
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

        public User GetUserByUsername(string username)
        {
            return _inMemoryUsers.FirstOrDefault(u => u.Username == username);
        }

        public Room GetRoomById(int roomId)
        {
            return _inMemoryRooms.FirstOrDefault(r => r.RoomId == roomId);
        }
    }
}

