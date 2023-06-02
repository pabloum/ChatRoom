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
		private readonly IReservationValidator _reservationValidator;

        public RoomService(IRoomRepository roomRepository, IReservationValidator reservationValidator)
		{
			_roomRepository = roomRepository;
			_reservationValidator = reservationValidator;
        }

		public IEnumerable<RoomDTO> SeeReservations()
		{
			return _roomRepository.SeeReservations().Select(r => r.MapToRoomDTO());
		}

        public RoomDTO GetReservationById(int id)
		{
			return _roomRepository.GetReservationById(id).MapToRoomDTO();
		}

		public RoomDTO MakeReservation(RoomDTO newReservationDto)
		{
			var newReservation = newReservationDto.MapToRoom();
			_reservationValidator.IsReservationPossible(newReservation);
			return _roomRepository.MakeReservation(newReservation).MapToRoomDTO();
			
        }

		public RoomDTO UpdatePutReservation(int id, RoomDTO updatedReservationDto)
		{
			if (id != updatedReservationDto.RoomId)
			{
				throw new ValidationException("Id in url and body don't match");
			}

            var newReservation = updatedReservationDto.MapToRoom();
            _reservationValidator.IsReservationPossible(newReservation);
            return _roomRepository.UpdatePutReservation(id, newReservation).MapToRoomDTO();
        }

		public string CancelReservation(int id)
		{
			return _roomRepository.CancelReservation(id);
        }
	}
}

