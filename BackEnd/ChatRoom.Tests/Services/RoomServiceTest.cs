using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.Mappers;
using ChatRoom.Entities.DTO;
using ChatRoom.Repository.Contracts;
using ChatRoom.Services.Providers.Contracts;
using ChatRoom.Services.Services;
using ChatRoom.Services.Services.Contracts;
using ChatRoom.Tests.DataFactory;
using Moq;

namespace ChatRoom.Tests.Services
{
	public class RoomServiceTest
	{
        private Mock<IRoomRepository> _mockRepository;
        private Mock<ITimeProvider> _timeProviderMock;
        private IRoomService _roomService;

		public RoomServiceTest()
		{
            _mockRepository = new Mock<IRoomRepository>();

            _timeProviderMock = new Mock<ITimeProvider>();
            _timeProviderMock.Setup(t => t.GetCurrentDateTime()).Returns(new DateTime(2023, 09, 19));
        }

        [Fact]
        public void TestGetAllRooms()
        {
            //Arrange
            _mockRepository.Setup(r => r.SeeReservations()).Returns(MockedDataFactory.GetMockedRooms());
            _roomService = new RoomService(_mockRepository.Object);

            //Act
            var results = _roomService.SeeReservations();

            //Assert
            Assert.NotNull(results);
            Assert.Equal(results.Count(), 3);
        }
    }
}

