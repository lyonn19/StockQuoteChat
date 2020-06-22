using Microsoft.AspNetCore.SignalR;
using StockMarketBot.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Web.Hubs
{
    public class ChatHub : Hub
    {
        IStockService stockService;

        public async Task SendMessage(string user, string message)
        {

            await Clients.All.SendAsync("ReceiveMessage", user, message);
            if (message.StartsWith("/stock="))
            {
                stockService = new StockService(); // TODO Dependency Injection
                await stockService.GetStockQuote(message.Trim().Substring(7));
            }
        }
    }
}
