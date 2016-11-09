using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStockMarket
{
    /// <summary>
    /// Class representing a common stock in the market
    /// </summary>
    public class CommonStock
    {
        public string StockSymbol { get; set; }
        public double LastDividend { get; set; }
        public int ParValue { get; set; }
        public List<Trade> Trades { get; set; }

        /// <summary>
        /// Class to store the data for a Common stock
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="lastDividend"></param>
        /// <param name="parValue"></param>
        public CommonStock(string symbol, double lastDividend, 
            int parValue)
        {
            StockSymbol = symbol;

            // Check LastDividend is non-negative
            Helpers.CheckQuantityPositive(lastDividend);

            LastDividend = lastDividend;
            ParValue = parValue;
            Trades = new List<Trade>();
        }

        /// <summary>
        /// Calculate dividend yield for stock for a given price.
        /// </summary>
        /// <param name="price">Current price</param>
        /// <returns>The stock's dividend yield</returns>
        public virtual double CalculateDividendYield(double price)
        {
            // Check price is strictly greater than 0
            Helpers.CheckQuantityStrictlyPositive(price);

            return LastDividend / price;
        }

        /// <summary>
        /// Calculate Price Earnings ratio for stock.
        /// 
        /// Assume value is zero if last dividend is zero.
        /// </summary>
        /// <param name="price">Current price</param>
        /// <returns>PE ratio</returns>
        public double CalculatePriceEarningsRatio(double price)
        {
            // Check price is non-negative
            Helpers.CheckQuantityPositive(price);

            // return 0 if last dividend is zero
            return LastDividend == 0 ? 0 : price / LastDividend;
        }
        

        /// <summary>
        /// Add a new Trade for this stock
        /// </summary>
        /// <param name="trade">A Trade object containing details of the new trade</param>
        public void AddNewTrade(Trade trade)
        {
            Trades.Add(trade);
        }

        /// <summary>
        /// Calcultes Volume Weighted Stock Price for trades.
        /// 
        /// Assume value is zero if no trades have taken place.
        /// </summary>
        /// <returns></returns>
        public double CalculateVolumeWeightedStockPrice()
        {
            var tradesInLastFiveMinutes = GetTradesInLastFiveMinutes();

            // return 0 if no trades in last 5 minutes
            if (tradesInLastFiveMinutes.Count == 0) { return 0; }

            // Array containing each trade's contribution to numerator of Volume Weighted Stock Price
            var VWSPValues = tradesInLastFiveMinutes.Select(t => t.Price * t.Quantity).ToList();

            // Array containing quantities of each Trade
            var quantities = tradesInLastFiveMinutes.Select(t => t.Quantity).ToList();

            return VWSPValues.Sum() / quantities.Sum();
        }

        /// <summary>
        /// Return trades which have happened in last 5 minutes
        /// </summary>
        /// <returns></returns>
        protected List<Trade> GetTradesInLastFiveMinutes()
        {
            return Trades.Where(trade => trade.TimeStamp > DateTime.Now.AddMinutes(-5) &&
            trade.TimeStamp <= DateTime.Now).ToList();
        }

    }
}
