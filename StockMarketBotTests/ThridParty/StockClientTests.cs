using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockMarketBot.Agents;
using StockMarketBot.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketBot.Agents.Tests
{
    [TestClass()]
    public class StockClientTests
    {
        [TestMethod()]
        public async Task GetStockQuoteSimpleTestAsync()
        {
            // Arrange
            IStockClient service = new StockClient();
            string stockQuote = "aapl.us";

            // Act 
            var actual = await service.GetStockQuote(stockQuote);

            // Assert
            Assert.AreEqual("AAPL.US", actual.Symbol);
        }

        [TestMethod()]
        public async Task Should_ReturnQuoteObject_When_StockQuoteNameIsOK()
        {
            // Arrange
            IStockClient service = new StockClient();
            string stockQuote = "aapl.us"; // quote name ok 

            // Act 
            var actual = await service.GetStockQuote(stockQuote);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(Quote));
        }

        [TestMethod()]
        public async Task Should_ReturnNotUnderstoodMessage_When_StockQuoteNameIsWrong()
        {
            // Arrange
            IStockClient service = new StockClient();
            string stockQuote = "appl.us"; // quote name wrong 

            // Act 
            var actual = await service.GetStockQuote(stockQuote);

            // Assert
            Assert.AreEqual("We couldn´t understood your query, check your stock name", actual.Message);
        }

    }
}