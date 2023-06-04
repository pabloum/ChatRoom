using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Services.Base;

namespace ChatRoom.Services.Services.Contracts
{
	public interface IRoomService : IService
	{
        IEnumerable<RoomDTO> SeeReservations();
        RoomDTO GetById(int roomId);
        RoomDTO CreateRoom(RoomDTO roomDTO);
    }
}

