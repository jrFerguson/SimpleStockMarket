using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SimpleStockMarket.Tests
{
    [TestFixture]
    public class StockMarketTests
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void TestInvalidPriceInYieldCalcThrowsException(double price)
        {
            CommonStock commonStock = new CommonStock("Test", 8, 100);
            CommonStock preferedStock = new PreferredStock("Test", 8, 100, 0.02);

            Assert.Throws<ArgumentOutOfRangeException>(() => commonStock.CalculateDividendYield(price));
            Assert.Throws<ArgumentOutOfRangeException>(() => preferedStock.CalculateDividendYield(price));
        }

        [Test]
        public void TestConstructorsThrowExceptionIfArgumentsInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CommonStock("test", -1,
            100));

            Assert.Throws<ArgumentOutOfRangeException>(() => new PreferredStock("test", 1,
            100, -0.02));

            Assert.Throws<ArgumentOutOfRangeException>(() => new Trade(-1, TradeIndicator.Buy,
            10));

            Assert.Throws<ArgumentOutOfRangeException>(() => new Trade(1, TradeIndicator.Buy,
            -10));
        }

        [TestCase(0, 100, 8, 0)]
        [TestCase(100, 100, 8, 0.08)]
        public void TestPERatioCalculation(double lastdividend, int parValue, double price, 
            double expectedValue)
        {
            CommonStock commonStock = new CommonStock("Test", lastdividend, parValue);

            Assert.AreEqual(commonStock.CalculatePriceEarningsRatio(price), expectedValue);
        }

        [TestCase(0, 100, 8, 0)]
        [TestCase(100, 100, 10, 10)]
        public void TestCommonShareDividendYieldCalculation(double lastdividend, int parValue, double price,
            double expectedValue)
        {
            CommonStock commonStock = new CommonStock("Test", lastdividend, parValue);

            Assert.AreEqual(commonStock.CalculateDividendYield(price), expectedValue);
        }

        [TestCase(0, 100, 8, 0)]
        [TestCase(0.1, 100, 5, 2)]
        public void TestPreferredShareDividendYieldCalculation(double fixedDividend, int parValue, double price,
            double expectedValue)
        {
            PreferredStock preferredStock = new PreferredStock("Test", 10, parValue, fixedDividend);

            Assert.AreEqual(preferredStock.CalculateDividendYield(price), expectedValue);
        }

        [Test]
        public void TestVolumeWeightedStockPrice()
        {
            CommonStock commonStock = new CommonStock("Test", 8, 100);

            var now = DateTime.Now;

            var oneMinsBeforeNow = now.AddMinutes(-1);
            var twoMinsBeforeNow = now.AddMinutes(-2);
            var threeMinsBeforeNow = now.AddMinutes(-3);

            var firstTrade = new Trade(1, TradeIndicator.Buy, 10, oneMinsBeforeNow);
            var secondTrade = new Trade(2, TradeIndicator.Buy, 10, twoMinsBeforeNow);
            var thirdTrade = new Trade(3, TradeIndicator.Buy, 10, threeMinsBeforeNow);

            commonStock.AddNewTrade(firstTrade);
            commonStock.AddNewTrade(secondTrade);
            commonStock.AddNewTrade(thirdTrade);

            Assert.AreEqual(commonStock.CalculateVolumeWeightedStockPrice(), 10);
        }

        [Test]
        public void TestVWSPNoTradesInLastFiveMinutes()
        {
            CommonStock commonStock = new CommonStock("Test", 8, 100);

            var now = DateTime.Now;
            
            var sixMinsFromNow = now.AddMinutes(6);
            var sevenMinsFromNow = now.AddMinutes(7);
            var sixMinsBeforeNow = now.AddMinutes(-6);

            var firstTrade = new Trade(1, TradeIndicator.Buy, 10, sixMinsFromNow);
            var secondTrade = new Trade(1, TradeIndicator.Buy, 10, sevenMinsFromNow);
            var thirdTrade = new Trade(1, TradeIndicator.Buy, 10, sixMinsBeforeNow);

            commonStock.AddNewTrade(firstTrade);
            commonStock.AddNewTrade(secondTrade);
            commonStock.AddNewTrade(thirdTrade);

            Assert.AreEqual(commonStock.CalculateVolumeWeightedStockPrice(), 0);
        }

        [Test]
        public void TestGeometricMeanOfVWSP()
        {
            CommonStock commonStock1 = new CommonStock("Test", 8, 100);
            CommonStock commonStock2 = new CommonStock("Second Test", 10, 60);

            var now = DateTime.Now;

            var oneMinsBeforeNow = now.AddMinutes(-1);
            var twoMinsBeforeNow = now.AddMinutes(-2);
            var threeMinsBeforeNow = now.AddMinutes(-3);
            var fourMinsBeforeNow = now.AddMinutes(-4);

            var firstTrade = new Trade(1, TradeIndicator.Buy, 10, oneMinsBeforeNow);
            var secondTrade = new Trade(2, TradeIndicator.Buy, 10, twoMinsBeforeNow);
            var thirdTrade = new Trade(3, TradeIndicator.Buy, 10, threeMinsBeforeNow);
            var fourthTrade = new Trade(4, TradeIndicator.Buy, 10, fourMinsBeforeNow);
            var fifthTrade = new Trade(6, TradeIndicator.Buy, 20, fourMinsBeforeNow);

            commonStock1.AddNewTrade(firstTrade);
            commonStock1.AddNewTrade(secondTrade);
            commonStock1.AddNewTrade(thirdTrade);

            commonStock2.AddNewTrade(fourthTrade);
            commonStock2.AddNewTrade(fifthTrade);

            StockMarket market = new StockMarket(new List<CommonStock> { commonStock1, commonStock2});

            Assert.AreEqual(market.CalculateGBCEWeightedStockPrice(), Math.Sqrt(160), 0.001);
        }
    }
}
