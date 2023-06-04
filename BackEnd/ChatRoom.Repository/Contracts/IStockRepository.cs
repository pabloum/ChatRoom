using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IStockRepository : IRepository
	{
        Stock GetStock(string stockCode);
    }
}

