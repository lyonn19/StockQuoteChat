using StockMarketBot.Agents.Base;
using StockMarketBot.Models;
using StockMarketBot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace StockMarketBot.Agents
{
    public class StockClient : ServiceBase, IStockClient, IDisposable
    {
        public StockClient() : base(Settings.API_BASE)
        {
        }

        public async Task<Quote> GetStockQuote(string stock_code)
        {
            var quote = new Quote();
            try
            {
                var fileInfo = new FileInfo($"{stock_code}.csv");

                var response = await GetAsync(Settings.API_URL, new Dictionary<string, string>
                {
                    { "s", stock_code },
                    { "f", "sd2t2ohlcv&h" },
                    { "e", "csv" }
                });

                if (response.IsSuccessStatusCode)
                {
                    await CreateCsvFile(fileInfo, response);

                    List<CsvMappingResult<Quote>> result = ParseCsvFile(fileInfo);

                    if (result.Any())
                    {
                        quote = new Quote
                        {
                            Symbol = result[1].Result.Symbol,
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
                else
                {
                    // Manage Excepcion 
                    quote = new Quote
                    {
                        Status = false,
                        Message = "We couldn´t understood your query, check your stock name"
                    };
                }
            }
            catch
            {
                quote = new Quote
                {
                    Status = false,
                    Message = "An unexpected error occurred"
                };
            }

            return quote;
        }

        private static async Task CreateCsvFile(FileInfo fileInfo, HttpResponseMessage response)
        {
            using var content = await response.Content.ReadAsStreamAsync();
            using var fs = File.Create(fileInfo.FullName, 1024, FileOptions.WriteThrough);
            content.Seek(0, SeekOrigin.Begin);
            content.CopyTo(fs);
        }

        private static List<CsvMappingResult<Quote>> ParseCsvFile(FileInfo fileInfo)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ',');
            CsvQuoteParsing csvMapper = new CsvQuoteParsing();
            CsvParser<Quote> csvParser = new CsvParser<Quote>(csvParserOptions, csvMapper);

            return csvParser
                .ReadFromFile(fileInfo.FullName, Encoding.ASCII)
                .ToList();
        }
    }
}
