using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChatRoom.Common.HttpRequest
{
	public interface IRequestHandler
	{
        Task<HttpResponseMessage> RequestStock(string url);
    }

	public class RequestHandler : IRequestHandler
    {
        private HttpClient _client;

		public RequestHandler(HttpClient client)
		{
            _client = client;
            _client.BaseAddress = new Uri("https://stooq.com/q/l/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage?> RequestStock(string stockCode)
        {
            var result = await _client.GetAsync($"?s={stockCode}&f=sd2t2ohlcv&h&e=csv");
            return result;
        }

    }
}

