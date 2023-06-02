using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Contracts;
using ChatRoom.Persistence.InMemoryData;
using Microsoft.EntityFrameworkCore;
using ChatRoom.Persistence.Context;
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Repository
{
    public class RoomRepository : IRoomRepository
	{
        private readonly HotelBookingDbContext _context;
        private readonly bool _useDataBase;
        private IInMemoryData _inMemoryData;

        private readonly DbSet<Room> DbSet;

        public RoomRepository(HotelBookingDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
        {
            _context = context;
            _inMemoryData = inMemoryData;

            _useDataBase = configuration.GetSection("UseDataBase").Value == "True";

            if (_useDataBase) { DbSet = _context.Set<Room>(); }
        }

        public IEnumerable<Room> SeeReservations()
        {

            return _useDataBase ? DbSet.AsNoTracking() : _inMemoryData.GetAll();
        }
    }
}

