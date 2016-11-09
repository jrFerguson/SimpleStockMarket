using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    /// <summary>
    /// Class to store the data for a Preferred stock
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="lastDividend"></param>
    /// <param name="parValue"></param>
    public class PreferredStock : CommonStock
    {
        public double FixedDividend { get; set; }

        public PreferredStock(string symbol, uint lastDividend,
            int parValue, double fixedDividend) 
            : base(symbol, lastDividend, parValue)
        {
            // Check FixedDividend is non-negative
            Helpers.CheckQuantityPositive(fixedDividend);

            FixedDividend = fixedDividend;
        }

        /// <summary>
        /// Calculate dividend yield for preferred share
        /// </summary>
        /// <param name="price">Current price</param>
        /// <returns>The stock's dividend yield</returns>
        public override double CalculateDividendYield(double price)
        {
            // Check price is strictly greater than 0
            Helpers.CheckQuantityStrictlyPositive(price);

            return FixedDividend * ParValue / price;
        }
       
    }
}
