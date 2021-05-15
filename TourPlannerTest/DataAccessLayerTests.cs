using NUnit.Framework;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;
using System.Configuration;

namespace TourPlannerTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void httpListenerNotNull()
        {
            httpListener http = httpListener.Instance();
            Assert.NotNull(http);
        }
        [Test]
        public void DBConnNotNull()
        {
            DBConn db = DBConn.Instance();
            Assert.IsNotNull(db);
        }
        [Test]
        public void GetRoutesNotNull()
        {
            DBOutput dbOut = new DBOutput();
            TourPlannerManager mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            MediaFolder folder = mediaManager.GetMediaFolder("");
            Assert.IsNotNull(dbOut.GetRoutes(folder));
        }
    }
}