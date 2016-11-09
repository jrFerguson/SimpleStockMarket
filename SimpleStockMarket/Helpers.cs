using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStockMarket
{
    /// <summary>
    /// Class containing helper methods.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Method which checks if a monetary quantity is a positive value.
        /// </summary>
        /// <param name="value"></param>
        public static void CheckQuantityPositive(double value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Monetary quantity provided must be positive.");
            }
        }

        /// <summary>
        /// Method which checks if a monetary quantity is strictly greater than zero.
        /// </summary>
        /// <param name="value"></param>
        public static void CheckQuantityStrictlyPositive(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Monetary quantity provided must be positive.");
            }
        }
    }
}
