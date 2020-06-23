using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Agents.Base
{
    public class BaseClient 
    {
        private HttpClient _httpClient;
        private string _endPoint;

        public BaseClient(string endPoint)
        {
            _endPoint = endPoint;
            _httpClient = MakeHttpClient();
        }

        private HttpClient MakeHttpClient()
        {
            _httpClient = new HttpClient
            {
                MaxResponseContentBufferSize = 256000, 
                Timeout = TimeSpan.FromMilliseconds(60000)
            };

            return _httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string method, Dictionary<string, string> parameters)
        {
            var uri = new Uri(BuidlQueryString(_endPoint + method, parameters));
            return await _httpClient.GetAsync(uri);
        }

        private string BuidlQueryString(string endPoint, Dictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                return string.Format("{0}?{1}", endPoint, string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
            }
            else
            {
                return endPoint;
            }
        }
        
    }
}
