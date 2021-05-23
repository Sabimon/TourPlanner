using NUnit.Framework;
using TourPlannerModels;
using TourPlannerDL;
using TourPlannerBL;

namespace TourPlannerTest
{
    [TestFixture]
    public class DBTest
    {
        private DBOutput dbOut = new DBOutput();

        [Test]
        public void GetRoutesNotNull()
        {
            TourPlannerManager tourManager = TourPlannerManagerFactory.GetFactoryManager();
            MediaFolder folder = tourManager.GetMediaFolder();
            Assert.IsNotNull(dbOut.GetRoutes());
        }
        [Test]
        public void GetRouteIDNotNull()
        {
            Assert.IsNotNull(dbOut.GetRouteID("test"));
        }
        [Test]
        public void CountRouteIDInDescriptionIsZero()
        {
            Assert.AreEqual(dbOut.CountRouteIDInDescription(0), 0);
        }
    }
}
