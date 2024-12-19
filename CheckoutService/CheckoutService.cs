using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.Pricing;

namespace Checkout.Checkout
{
    public class CheckoutService
    {
        /*
         * As the items are characters we use a
         * list of characters to define items.
         * Similarly pricing rules are taken from
         * the respective file in an array that
         * corresponds to each character
         */

        private PricingRule[] _pricingRules;
        private List<char> _items;

        public CheckoutService(IEnumerable<PricingRule> pricingRules)
        {
            _pricingRules = pricingRules.ToArray();
            _items = new List<char>();
        }

        //Using this allows to add items dynamically rather than hardcoding items

        public void Scan(char item)
        {
            _items.Add(item);
        }

        public decimal GetTotalPrice()
        {
            //Groups all items in the list alphabetically to ensure that all of the same items are next to each other

            decimal total = 0;
            var itemsGroups = _items.GroupBy(x => x);


            //Using @ allows for protected keywords to be used
            foreach (var @group in itemsGroups)
            {
                var rules = _pricingRules
                    .Where(r => r.Item == @group.Key)
                    .OrderByDescending(r => r.Count);
                //Counts the number of each type of different item within the list
                var itemCount = @group.Count();

                while (itemCount > 0)
                {
                    //Adds up all the prices of items, and ensures that special offers are added correctly
                    var ruleToApply = rules.First(r => r.Count <= itemCount);
                    total += ruleToApply.Price;
                    itemCount -= ruleToApply.Count;
                }
            }
            return total;
        }
    }
}
