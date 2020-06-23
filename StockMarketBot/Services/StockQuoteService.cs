using StockMarketBot.Agents;
using StockMarketBot.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        private IStockClient _stockClient;
        private Producer _producer;

        public StockQuoteService() 
        {
            _stockClient = new StockClient();
            _producer = new Producer();
        }

        public async Task GetStockQuote(string stockCode)
        {
            var quote = await _stockClient.GetStockQuote(stockCode);
            if (quote.Status)
                _producer.PushMessageToQ($"{quote.Symbol} quote is {quote.Close} per share");
            else
                _producer.PushMessageToQ(quote.Message);
        }

    }
}
