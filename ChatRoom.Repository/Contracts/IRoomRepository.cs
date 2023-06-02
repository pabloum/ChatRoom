using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Repository.Base;

namespace ChatRoom.Repository.Contracts
{
	public interface IRoomRepository : IRepository 
	{
        IEnumerable<Room> SeeReservations();
        Room GetReservationById(int id);
        Room MakeReservation(Room newReservation);
        Room UpdatePutReservation(int id, Room updatedReservation);
        string CancelReservation(int id);
    }
}

