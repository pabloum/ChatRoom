using ChatRoom.Entities.DTO;
using ChatRoom.Entities.Exceptions;
using ChatRoom.Entities.Mappers;
using ChatRoom.Repository.Contracts;
using ChatRoom.Services.Services.Contracts;

namespace ChatRoom.Services.Services
{
	public class RoomService : IRoomService
    {
		private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
		{
			_roomRepository = roomRepository;
        }

        public RoomDTO CreateRoom(RoomDTO roomDTO)
        {
            return _roomRepository.CreateRoom(roomDTO.MapToRoom()).MapToRoomDTO();
        }

        public IEnumerable<RoomDTO> SeeReservations()
		{
			return _roomRepository.SeeReservations().Select(r => r.MapToRoomDTO());
		}
	}
}