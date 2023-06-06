using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Entities.DTO;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
	{
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;

        }

        [HttpGet("{roomId}")]
        public ActionResult<IEnumerable<User>> GetMessagesByRoom(int roomId)
        {
            var result = _messageService.GetMessagesByRoom(roomId);
            return Ok(result);
        }

        [HttpPost("{roomId}")]
        public ActionResult<RoomDTO> CreateMessageInRoom(int roomId, [FromBody] MessageDTO message)
        {
            var result = _messageService.CreateMessage(roomId, message);
            return Ok(result);
        }
    }
}

