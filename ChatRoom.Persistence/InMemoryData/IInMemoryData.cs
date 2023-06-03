﻿using ChatRoom.Entities.Domain;

namespace ChatRoom.Persistence.InMemoryData
{
    public interface IInMemoryData
    {
        IEnumerable<Room> GetAllRooms();
        Room CreateRoom(Room room);
        IEnumerable<Stock> GetAllStocks();
        Stock GetStockByStockCode(string stockCode);
        Message CreateMessage(Message message);
        IEnumerable<Message> GetMessegesByRoom(int roomId);
        User CreateUser(User user);
        User GetUserById(int userId);
    }
}

