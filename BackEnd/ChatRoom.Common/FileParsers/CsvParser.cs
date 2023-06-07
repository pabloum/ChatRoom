using System;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Reflection.Metadata;
using ChatRoom.Entities.Domain;

namespace ChatRoom.Common.FileParsers
{
	public interface ICsvParser
    {
        Stock ParseCsvToStock(string csvData);
    }

    public class CsvParser : ICsvParser
    {
		public Stock ParseCsvToStock(string csvData)
		{
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

                return stocks.FirstOrDefault();
            }
        }
	}
}

