using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace StockMarketBot.Agents.Base
{
    public class ServiceBase : RestService
    {
        public const string PAGE_NUMBER = "X-Page-Number";
        public const string PAGE_SIZE = "X-Page-Size";

        public ServiceBase(string endPoint) : base(endPoint)
        {
        }

        protected override HttpClient MakeHttpClient(HttpMessageHandler handler)
        {
            var client = new HttpClient();

            return client;
        }

    }
}
