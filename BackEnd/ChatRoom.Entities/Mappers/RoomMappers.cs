using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Entities.Mappers
{
	public static class RoomMappers
	{
		public static Room MapToRoom(this RoomDTO roomDTO)
		{
			return new Room
			{
				RoomId = roomDTO.RoomId,
				RoomName = roomDTO.RoomName
            };
		}

        public static RoomDTO MapToRoomDTO(this Room room)
        {
			return new RoomDTO
			{
				RoomId = room.RoomId,
				RoomName = room.RoomName
            };
        }
    }
}

