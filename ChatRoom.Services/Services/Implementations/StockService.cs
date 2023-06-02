using System;
using ChatRoom.Entities.Domain;

namespace ChatRoom.Services.Services
{
	public class StockService
    {
        Stock GetStock(string stockCode)
        {
            var stock = new Stock
            {
                DateTime = DateTime.Now,
                Open = (decimal)181.03,
                High = (decimal)181.78,
                Low = (decimal)179.26,
                Close = (decimal)180.93,
                Volume = 39457561,
            };

            return stock;
        }
    }
}

