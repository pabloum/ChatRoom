using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Entities.Mappers
{
	public static class RoomMappers
	{
		public static Room MapToRoom(this RoomDTO roomDTO)
		{
			return roomDTO == null ? null : new Room
			{
				RoomId = roomDTO.RoomId,
				StartReservation = roomDTO.StartReservation,
				EndReservation = roomDTO.EndReservation
            };
		}

        public static RoomDTO MapToRoomDTO(this Room room)
        {
			return room == null ? null : new RoomDTO
			{
				RoomId = room.RoomId,
                StartReservation = room.StartReservation,
                EndReservation = room.EndReservation
            };
        }
    }
}

