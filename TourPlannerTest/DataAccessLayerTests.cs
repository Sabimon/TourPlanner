using NUnit.Framework;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;

namespace TourPlannerTest
{
    public class Tests
    {
        httpListener http = new httpListener();
        DBOutput dbOut = new DBOutput();
        DBInput dbIn = new DBInput();

        [Test]
        public void httpListenerNotNull()
        {
            Assert.NotNull(http);
        }
        [Test]
        public void DBOutputNotNull()
        {
            Assert.NotNull(dbOut);
        }
        [Test]
        public void DBInputNotNull()
        {
            Assert.NotNull(dbIn);
        }
        [Test]
        public void GetRoutesNotNull()
        {
            TourPlannerManager mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            MediaFolder folder = mediaManager.GetMediaFolder("");
            Assert.NotNull(dbOut.GetRoutes(folder));
        }
    }
}