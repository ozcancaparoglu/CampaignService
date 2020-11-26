using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignService.UnitTests
{
    [TestFixture]
    public class CampaignServiceTests : TestBase
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ActiveCampaigns()
        {
            var result = await CampaignService.GetAllActiveCampaigns();
            Assert.AreEqual(50, result.Count());
        }

        [Test]
        public async Task ActiveShippingMethods()
        {
            var result = await ShippingMethodService.GetShippingMethods();
            Assert.AreEqual(2, result.Count());
        }
    }
}