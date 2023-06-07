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
                    stock.DateTime = fields[1] + "T" + fields[2];
                    stock.Open = fields[3];
                    stock.High = fields[4];
                    stock.Low = fields[5];
                    stock.Close = fields[6];
                    stock.Volume = fields[7];

                    stocks.Add(stock);
                }

                return stocks.FirstOrDefault();
            }
        }
	}
}

