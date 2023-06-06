using ChatRoom.Entities.Domain;

namespace ChatRoom.Persistence.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        Room CreateRoom(Room room);
        Message CreateMessage(Message message);
        IEnumerable<Message> GetMessegesByRoom(int roomId);
        User CreateUser(User user);
        User GetUserByUsername(string username);
    }
}

