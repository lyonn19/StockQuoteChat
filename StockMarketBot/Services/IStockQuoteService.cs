using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Services
{
    public interface IStockQuoteService
    {
        Task GetStockQuote(string stock_code);
    }
}
