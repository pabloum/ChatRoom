using System;
using Web.Providers.Contracts;
using Web.Providers.Entities;

namespace Web.Providers.Implementations
{
	public class StockProvider : IStockProvider
    {
        private ServiceHandler _serviceHandler;

        public StockProvider()
		{
			_serviceHandler = new ServiceHandler(); //TODO: use DI
		}

		public async Task<string> GetStockQuote(string stockCode = "aapl.us")
		{
            var response = await _serviceHandler.Get<string>($"api/Stock?stock_code={stockCode}");
			return response;
        }
	}
}

