﻿using Microsoft.AspNetCore.SignalR;
using StockMarketBot.Services;
using System.Threading.Tasks;

namespace StockQuoteChat.Hubs
{
    public class ChatHub : Hub
    {
        const int startIndex = 7;
        readonly IStockQuoteService _quoteService;

        public ChatHub(IStockQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            if (message.Trim().StartsWith("/stock="))
            {
                await CallStockMarketBot(message);
            }
        }

        private async Task CallStockMarketBot(string message)
        {
            await _quoteService.GetStockQuote(message.Trim().Substring(startIndex));
        }
    }
}
