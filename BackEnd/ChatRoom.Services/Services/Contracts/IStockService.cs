using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Services.Contracts
{
	public interface IStockService : IService
	{
        Task<string> GetStock(string stockCode);
	}
}

