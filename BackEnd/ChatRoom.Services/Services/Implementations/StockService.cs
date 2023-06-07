using ChatRoom.Services.Services.Contracts;
using ChatRoom.Common.HttpRequest;
using ChatRoom.Common.FileParsers;
using ChatRoom.Entities.DTO;

namespace ChatRoom.Services.Services
{
	public class StockService : IStockService
    {
        private readonly IRequestHandler _requestHandler;
        private readonly ICsvParser _csvParser;
        private readonly IMessageService _meesageService;

        public StockService(IRequestHandler requestHandler, ICsvParser csvParser, IMessageService messageService)
        {
            _requestHandler = requestHandler;
            _csvParser = csvParser;
            _meesageService = messageService;
        }

        public async Task<string> GetStock(int roomId, string stockCode)
        {
            //var stockCodeExample = "aapl.us";
            var response = await _requestHandler.RequestStock(stockCode);

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    var stock = _csvParser.ParseCsvToStock(await content.ReadAsStringAsync());
                    var quote = $"{stock.Code} quote is ${stock.Close} per share";
                    _meesageService.CreateMessage(roomId, new MessageDTO
                    {
                        RoomId = roomId,
                        MessagePrompt = quote,
                        PostingTime = DateTime.Now,
                        Username = "TheStockBot"
                    });
                    return quote;
                }
            }

            return String.Empty;
        }
    }
}