using System;

namespace StockMarketBot.Models
{
    public class Quote : Response
    {
        public string Symbol { get; set; }

        public DateTime DateTime { get; set; }

        public string Time { get; set; }

        public decimal Open { get; set; }
        
        public decimal High { get; set; }
        
        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public Int32 Volume { get; set; }

    }
}
