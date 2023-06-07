using System;

namespace Web.Providers.Contracts
{
	public interface IStockProvider
	{
        Task<string> GetStockQuote(int roomId, string stockCode = "aapl.us");
    }
}

