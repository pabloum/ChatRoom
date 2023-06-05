using System;
namespace Web.Providers.Contracts
{
	public interface IStockProvider
	{
        Task<string> GetStockQuote(string stockCode = "aapl.us");
    }
}

