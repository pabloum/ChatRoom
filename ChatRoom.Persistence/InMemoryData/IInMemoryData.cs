using ChatRoom.Entities.Domain;

namespace ChatRoom.Persistence.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAll();
    }
}

