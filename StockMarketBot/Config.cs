using System;
using System.Collections.Generic;
using System.Text;

namespace StockMarketBot
{
    public static class Settings
    {
        public static string API_BASE = "https://stooq.com";
        public static string API_URL = "/q/l/";
        public static string parFormat = "&f=sd2t2ohlcv&h&e=csv";
    }
}
