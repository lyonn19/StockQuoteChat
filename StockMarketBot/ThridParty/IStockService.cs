using StockMarketBot.Models;
using System.Threading.Tasks;

namespace StockMarketBot.Agents
{
    public interface IStockService
    {
        Task<Quote> GetStockQuote(string stock_code); 
    }
}
