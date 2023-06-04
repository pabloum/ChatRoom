using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IRoomRepository : IRepository 
	{
        IEnumerable<Room> SeeReservations();
        Room GetById(int roomId);
        Room CreateRoom(Room room);
    }
}

