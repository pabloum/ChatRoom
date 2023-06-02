using System;
namespace ChatRoom.Entities.Domain
{
	public class Room
	{
		public int RoomId { get; set; }
		public DateTime StartReservation { get; set; }
		public DateTime EndReservation { get; set; }
    }
}

