using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.Context;
using ChatRoom.Persistence.InMemoryData;
using ChatRoom.Repository.Base;
using ChatRoom.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChatRoom.Repository
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(ChatRoomDbContext context, IInMemoryData inMemoryData, IConfiguration configuration)
            : base(context, inMemoryData, configuration)
        {
        }

        public Stock GetStock(string stockCode)
        {
            return _useDataBase ?
                    DbSet.AsNoTracking().FirstOrDefault(s => s.Code == stockCode)
                 : _inMemoryData.GetStockByStockCode(stockCode);
        }
    }
}

