using NUnit.Framework;

namespace BscScanner.Extensions.Convert.Tests {
    public class BscConvertTests {

        [Test]
        public void RunGweiToBnbConversion() {
            var gwei = 15700000000000000f;
            var bnb = BscConvert.GweiToBnb((decimal) gwei);

            Assert.AreEqual((decimal) 0.0157f, bnb);
        }

        [Test]
        public void RunBnbToGweiConversion() {
            const double bnb = 0.0157;
            var gwei = BscConvert.BnbToGwei((decimal) bnb);

            Assert.AreEqual((decimal) 15700000000000000, gwei);
        }
    }
}