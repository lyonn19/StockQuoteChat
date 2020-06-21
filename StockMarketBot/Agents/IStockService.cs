using StockMarketBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Agents
{
    public interface IStockService
    {
        Task<Quote> GetStockQuote(string stock_code); 
    }
}
