using System;

namespace SimpleStockMarket
{
    /// <summary>
    /// Indicator as to whether the trade is buy or sell
    /// </summary>
    public enum TradeIndicator
    {
        Buy,
        Sell
    }

    /// <summary>
    /// Class to store a details of a given trade.
    /// </summary>
    public class Trade
    {
        public DateTime TimeStamp { get; private set; } 
        public int Quantity { get; private set; }
        public TradeIndicator Indicator { get; private set; }
        public double Price { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="quantity">Quantity sold (assumed grearer than 0)/param>
        /// <param name="indicator">Buy/Sell</param>
        /// <param name="price">Price sold (assumed grearer than 0)</param>
        public Trade(int quantity, TradeIndicator indicator,
            double price, DateTime timeStamp)
        {
            // Set TimeStamp to the current time
            TimeStamp = timeStamp;

            // Check Quantity is non-negative
            Helpers.CheckQuantityPositive(quantity);
            // Check Price is non-negative
            Helpers.CheckQuantityPositive(price);

            Quantity = quantity;
            Indicator = indicator;
            Price = price;
        }

        /// <summary>
        /// Constructor without time stamp parameter.
        /// 
        /// Time stamp is set to the current time.
        /// </summary>
        /// <param name="quantity">Quantity sold (assumed grearer than 0)/param>
        /// <param name="indicator">Buy/Sell</param>
        /// <param name="price">Price sold (assumed grearer than 0)</param>
        public Trade(int quantity, TradeIndicator indicator, 
            double price)
        {           
            // Set TimeStamp to the current time
            TimeStamp = DateTime.Now;

            // Check Quantity is strictly greater than 0
            Helpers.CheckQuantityStrictlyPositive(quantity);
            // Check Price is strictly greater than 0
            Helpers.CheckQuantityStrictlyPositive(price);

            Quantity = quantity;
            Indicator = indicator;
            Price = price;
        }
    }
}
