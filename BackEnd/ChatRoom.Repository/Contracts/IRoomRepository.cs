using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IRoomRepository : IRepository 
	{
        IEnumerable<Room> SeeReservations();
        Room CreateRoom(Room room);
    }
}

