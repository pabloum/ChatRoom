using System;
using ChatRoom.Entities.Domain;
using ChatRoom.Services.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
	{
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
		{
			_stockService = stockService;
        }

		[HttpGet]
		public async Task<ActionResult<string>> GetStock(int roomId, string stock_code)
		{
			var stock = await _stockService.GetStock(roomId, stock_code);
			return Ok(stock);
		}
	}
}

