using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Pricing
{
    public class PricingRule
    {
        /*
         * This contains two constructors for pricing rules as it includes
         * a possibility of a single item being scanned or no items being scanned
         * with an empty pricing rule
         */

        public readonly char Item;
        public readonly int Count;
        public readonly decimal Price;

        public PricingRule(char item, decimal price) : this(item, 1, price)
        {

        }

        public PricingRule(char item, int count, decimal price)
        {
            Item = item;
            Count = count;
            Price = price;
        }
    }
}
