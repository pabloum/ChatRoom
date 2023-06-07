using ChatRoom.Entities.Domain;
using ChatRoom.Services.Services.Contracts;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using ChatRoom.Common.HttpRequest;
using ChatRoom.Common.FileParsers;

namespace ChatRoom.Services.Services
{
	public class StockService : IStockService
    {
        private IRequestHandler _requestHandler;
        private ICsvParser _csvParser;

        public StockService(IRequestHandler requestHandler, ICsvParser csvParser)
        {
            _requestHandler = requestHandler;
            _csvParser = csvParser;
        }

        public async Task<string> GetStock(string stockCode)
        {
            //var stockCodeExample = "aapl.us";
            var response = await _requestHandler.RequestStock(stockCode);

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    var stock = _csvParser.ParseCsvToStock(await content.ReadAsStringAsync());
                    return $"{stock.Code} quote is ${stock.Close} per share";
                }
            }

            return String.Empty;
        }
    }
}