using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Contracts;
using ChatRoom.Repository;
using ChatRoom.Services.Services.Contracts;

namespace ChatRoom.Services.Services
{
	public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Stock GetStock(string stockCode)
        {
            var stock = _stockRepository.GetStock(stockCode);
            return stock;
        }
    }
}