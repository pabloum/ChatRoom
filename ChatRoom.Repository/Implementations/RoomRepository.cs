using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Contracts;
using ChatRoom.Persistence.InMemoryData;
using Microsoft.EntityFrameworkCore;
using ChatRoom.Persistence.Context;
using Microsoft.Extensions.Configuration;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ChatRoomDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
            : base(context, inMemoryData, configuration)
        {
        }

        public IEnumerable<Room> SeeReservations()
        {
            return _useDataBase ? DbSet.AsNoTracking() : _inMemoryData.GetAllRooms();
        }
    }
}

