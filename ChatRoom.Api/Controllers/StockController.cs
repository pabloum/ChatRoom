using System;
using ChatRoom.Entities.Domain;
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

		[HttpPost]
		public ActionResult<Stock> GetStock(string stock_code)
		{

			return Ok(stock_code);
		}
	}
}

