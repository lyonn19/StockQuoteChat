using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Agents.Base
{
    public class RestService
    {
        private HttpClient _httpClient;
        private readonly string jsonMediaType = "application/json";
        private string _endPoint;

        public RestService(string endPoint)
        {
            _endPoint = endPoint;
            _httpClient = MakeHttpClient(null);
        }

        protected virtual HttpClient MakeHttpClient(HttpMessageHandler handler)
        {
            if (handler != null)
            {
                _httpClient = new HttpClient(handler);
            }
            else
            {
                _httpClient = new HttpClient();
            }

            _httpClient.MaxResponseContentBufferSize = 1656000; // 256000
            _httpClient.Timeout = TimeSpan.FromMilliseconds(120000);
            _httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));

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

        public HttpClient Client { get { return _httpClient; } }

    }
}
