using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStockMarket
{
    /// <summary>
    /// Class representing a simple stock market
    /// </summary>
    public class StockMarket
    {
        public List<CommonStock> Stocks { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StockMarket()
        {
            Stocks = new List<CommonStock>();
        }

        public StockMarket(List<CommonStock> stocks)
        {
            Stocks = stocks;
        }

        /// <summary>
        /// Calculate GBCE All Share Index
        /// </summary>
        /// <returns></returns>
        public double CalculateGBCEWeightedStockPrice()
        {
            // return 0 if no stocks
            if (Stocks.Count() == 0) { return 0d; }

            var root = Stocks.Count();

            var GBCEWeightedStockPrice = 1d;

            foreach (var stock in Stocks)
            {
                GBCEWeightedStockPrice *= Math.Pow(stock.CalculateVolumeWeightedStockPrice(), 1d / root);
            }

            return GBCEWeightedStockPrice;
        }
    }
}
