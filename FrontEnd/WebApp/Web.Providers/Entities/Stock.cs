using System;
namespace Web.Providers.Entities
{
	public class Stock
	{
        public string Code { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public long Volume { get; set; }
    }
}

