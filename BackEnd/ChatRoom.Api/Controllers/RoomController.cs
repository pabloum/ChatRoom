using System;
using ChatRoom.Entities.DTO;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetRooms")]
        public ActionResult<IEnumerable<RoomDTO>> GetRooms()
        {
            var result = _roomService.SeeReservations();
            return Ok(result);
        }

        [HttpGet("{roomId}")]
        public ActionResult<IEnumerable<RoomDTO>> GetRoomById(int roomId)
        {
            var result = _roomService.GetById(roomId);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<IEnumerable<RoomDTO>> CreateRoom([FromBody]RoomDTO roomDTO)
        {
            var result = _roomService.CreateRoom(roomDTO);
            return Ok(result);
        }
    }
}

