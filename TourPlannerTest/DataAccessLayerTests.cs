using NUnit.Framework;
using TourPlannerDL;

namespace TourPlannerTest
{
    public class Tests
    {
        httpListener http = new httpListener();
        DBConn db = new DBConn();

        [Test]
        public void httpListenerNotNull()
        {
            Assert.NotNull(http);
        }
        [Test]
        public void DBConnNotNull()
        {
            Assert.NotNull(db);
        }
    }
}