using ChatRoom.Entities.Domain;
using ChatRoom.Services.Services.Contracts;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace ChatRoom.Services.Services
{
	public class StockService : IStockService
    {
        public async Task<Stock> GetStock(string stockCode)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://stooq.com/q/l/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var stockCodeExample = "aapl.us";
            var response = await client.GetAsync($"?s={stockCode}&f=sd2t2ohlcv&h&e=csv");

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    var csvData = await content.ReadAsStringAsync();

                    List<Stock> stocks = new List<Stock>();

                    using (TextFieldParser parser = new TextFieldParser(new StringReader(csvData)))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        // Skip the header row if present
                        if (!parser.EndOfData)
                        {
                            parser.ReadLine();
                        }

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();

                            Stock stock = new Stock();
                            stock.Code = fields[0];
                            stock.DateTime = DateTime.Parse(fields[1] + "T" + fields[2]);

                            decimal.TryParse(fields[3], NumberStyles.Number, CultureInfo.InvariantCulture, out var Open);
                            decimal.TryParse(fields[4], NumberStyles.Number, CultureInfo.InvariantCulture, out var high);
                            decimal.TryParse(fields[5], NumberStyles.Number, CultureInfo.InvariantCulture, out var low);
                            decimal.TryParse(fields[6], NumberStyles.Number, CultureInfo.InvariantCulture, out var close);

                            stock.Open = Open;
                            stock.High = high;
                            stock.Low = low;
                            stock.Close = close;

                            stock.Volume = long.Parse(fields[7]);

                            stocks.Add(stock);
                        }
                    }

                    return stocks.FirstOrDefault();
                }
            }

            return default(Stock);
        }
    }
}