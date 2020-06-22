using Chat.Web.Messaging;
using Microsoft.AspNetCore.SignalR;
using StockMarketBot.Agents;
using StockMarketBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Web.Hubs
{
    public class ChatHub : Hub
    {
        const int startIndex = 7;
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            if (message.Trim().StartsWith("/stock="))
            {
                await CallBot(message);
            }
        }


        private async Task CallBot(string message)
        {
            var stockService = new StockQuoteService();
            await stockService.GetStockQuote(message.Trim().Substring(startIndex));
        }
    }
}
