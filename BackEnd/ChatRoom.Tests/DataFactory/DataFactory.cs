using System;
using ChatRoom.Entities.Domain;

namespace ChatRoom.Tests.DataFactory
{
    public static class MockedDataFactory
	{
		private static IEnumerable<Room> _rooms = new List<Room>
		{
			new Room{ RoomId = 1, RoomName = "Room1"},
			new Room{ RoomId = 2, RoomName = "Room2"},
			new Room{ RoomId = 3, RoomName = "Room3"},
        };

		public static IEnumerable<Room> GetMockedRooms()
		{
			return _rooms;
		}
	}
}

