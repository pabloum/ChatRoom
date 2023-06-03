using ChatRoom.Entities.Domain;

namespace ChatRoom.Persistence.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAllRooms();
        IEnumerable<Stock> GetAllStocks();
        Stock GetStockByStockCode(string stockCode);
    }
}

