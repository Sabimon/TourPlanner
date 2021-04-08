using NUnit.Framework;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;

namespace TourPlannerTest
{
    [TestFixture]
    public class Tests
    {
        httpListener http = httpListener.Instance();
        DBConn db = DBConn.Instance();
        DBOutput dbOut = new();
        DBInput dbIn = new();

        [Test]
        public void httpListenerNotNull()
        {
            Assert.NotNull(http);
        }
        [Test]
        public void DBConnNotNull()
        {
            Assert.IsNotNull(db);
        }
        [Test]
        public void GetRoutesNotNull()
        {
            TourPlannerManager mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            MediaFolder folder = mediaManager.GetMediaFolder("");
            Assert.IsNotNull(dbOut.GetRoutes(folder));
        }
    }
}