using Checkout.Checkout;
using Checkout.Pricing;
using NUnit.Framework;

namespace Checkout.CheckoutServiceTest
{
    public class CheckoutServiceTests
    {
        [Test]
        public void NoItemsScanned()
        {
            CheckoutService checkout = new CheckoutService(new List<PricingRule>());

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(0));
        }

        [Test]
        public void OneItemScanned()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('A', 50)
            };
            CheckoutService checkout = new CheckoutService(rules);

            checkout.Scan('A');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void OneOfEachItemScanned()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('A', 50),
                new PricingRule('B', 30),
                new PricingRule('C', 20),
                new PricingRule('D', 15)
            };
            CheckoutService checkout = new CheckoutService(rules);


            checkout.Scan('A');
            checkout.Scan('B');
            checkout.Scan('C');
            checkout.Scan('D');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50 + 30 + 20 + 15));
        }

        [Test]
        public void SpecialOfferItem()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('A', 3, 130)
            };
            CheckoutService checkout = new CheckoutService(rules);

            checkout.Scan('A');
            checkout.Scan('A');
            checkout.Scan('A');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
        }

        [Test]
        public void SpecialOfferWithExtraItemInBetween()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('A', 50),
                new PricingRule('B', 2, 45)
            };
            CheckoutService checkout = new CheckoutService(rules);

            checkout.Scan('B');
            checkout.Scan('A');
            checkout.Scan('B');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(45 + 50));
        }

        [Test]
        public void SpecialOfferPlusAdditionalOfTheSameItem()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('B', 2, 45),
                new PricingRule('B', 30)
            };
            CheckoutService checkout = new CheckoutService(rules);


            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('B');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(30 + 45));
        }

        [Test]
        public void MultipleSpecialOffers()
        {
            var rules = new List<PricingRule>()
            {
                new PricingRule('B', 2, 45)
            };
            CheckoutService checkout = new CheckoutService(rules);


            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('B');
            checkout.Scan('B');

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(45 + 45));
        }
    }
}
