using StockMarketBot.Agents.Base;
using StockMarketBot.Models;
using StockMarketBot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace StockMarketBot.Agents
{
    public class StockService : ServiceBase, IStockService
    {

        public StockService() : base(Settings.API_BASE)
        {
        }

        public async Task<Quote> GetStockQuote(string stock_code)
        {
            var quote = new Quote();
            try
            {
                var fileInfo = new FileInfo($"{stock_code}.csv");
                var parameters = new Dictionary<string, string>
                {
                    { "s", stock_code },
                    { "f", "sd2t2ohlcv&h" },
                    { "e", "csv" }
                };

                var response = await GetAsync(Settings.API_URL, parameters);

                if (response.IsSuccessStatusCode)
                {
                    using (var content = await response.Content.ReadAsStreamAsync())
                    {
                        using var fs = File.Create(fileInfo.FullName, 1024, FileOptions.WriteThrough);
                        content.Seek(0, SeekOrigin.Begin);
                        content.CopyTo(fs);
                    }

                    CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
                    CsvQuoteParsing csvMapper = new CsvQuoteParsing();
                    CsvParser<Quote> csvParser = new CsvParser<Quote>(csvParserOptions, csvMapper);

                    var result = csvParser
                        .ReadFromFile(fileInfo.FullName, Encoding.ASCII)
                        .ToList();

                    if (result != null)
                    {
                        quote = new Quote
                        {
                            Symbol = result[1]?.Result.Symbol,
                            DateTime = result[1].Result.DateTime,
                            Time = result[1].Result.Time,
                            Open = result[1].Result.Open,
                            High = result[1].Result.High,
                            Low = result[1].Result.Low,
                            Close = result[1].Result.Close,
                            Volume = result[1].Result.Volume,
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return quote;
        }
    }
}
