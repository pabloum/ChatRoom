using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Persistence.InMemoryData;

namespace ChatRoom.Persistence.InMemoryData
{
    public class InMemoryData : IInMemoryData
    {
        private List<Room> _inMemoryRoom;
        private List<Stock> _inMemoryStock;

        public InMemoryData()
        {
            _inMemoryRoom = new List<Room>
            {
                new Room { RoomId = 1, RoomName = "General Knowledge"},
                new Room { RoomId = 2, RoomName = "Sports"},
                new Room { RoomId = 3, RoomName = "Literature"},
                new Room { RoomId = 4, RoomName = "Programming"},
            };

            _inMemoryStock = new List<Stock>
            {
                new Stock
                {
                    Code = "aapl.us",
                    DateTime = DateTime.Now,
                    Open = (decimal)181.03,
                    High = (decimal)181.78,
                    Low = (decimal)179.26,
                    Close = (decimal)180.93,
                    Volume = 39457561,
                },
            };
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _inMemoryRoom;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _inMemoryStock;
        }

        public Stock GetStockByStockCode(string stockCode)
        {
            return _inMemoryStock.FirstOrDefault(s => s.Code == stockCode);
        }
    }
}

