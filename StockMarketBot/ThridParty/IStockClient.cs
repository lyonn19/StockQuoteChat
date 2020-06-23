using StockMarketBot.Models;
using System.Threading.Tasks;

namespace StockMarketBot.Agents
{
    public interface IStockClient
    {
        Task<Quote> GetStockQuote(string stockCode); 
    }
}
