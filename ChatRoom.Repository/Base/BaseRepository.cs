using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.Context;
using ChatRoom.Persistence.InMemoryData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Repository.Base
{
	public abstract class BaseRepository<T> : IRepository where T : class
    {
        protected readonly ChatRoomDbContext _context;
        protected readonly bool _useDataBase;
        protected IInMemoryData _inMemoryData;

        protected readonly DbSet<T> DbSet;

        public BaseRepository(ChatRoomDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
        {
            _context = context;
            _inMemoryData = inMemoryData;

            _useDataBase = configuration.GetSection("UseDataBase").Value == "True";

            if (_useDataBase) { DbSet = _context.Set<T>(); }
        }
	}
}

