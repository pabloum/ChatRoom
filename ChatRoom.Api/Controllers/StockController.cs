using System;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
	{
		public StockController()
		{
		}

		[HttpGet]
		public IActionResult GetStock(string stock_code)
		{

			return Ok(stock_code);
		}
	}
}

