using System;
using Web.Providers.Contracts;
using Entities;

namespace Web.Providers.Implementations
{
	public class StockProvider : IStockProvider
    {
        private IServiceHandler _serviceHandler;

        public StockProvider(IServiceHandler serviceHandler)
		{
			_serviceHandler = serviceHandler;
		}

		public async Task<string> GetStockQuote(int roomId, string stockCode = "aapl.us")
		{
			if (String.IsNullOrEmpty(stockCode)) stockCode = "aapl.us";
            var response = await _serviceHandler.Get<string>($"api/Stock?roomId={roomId}&stock_code={stockCode}");
			return response;
        }
	}
}

