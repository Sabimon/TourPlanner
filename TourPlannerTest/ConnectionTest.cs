using NUnit.Framework;
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
        public void ConnStringNotNull()
        {
            string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringConfig"].ConnectionString;
            Assert.IsNotNull(_connectionString);
        }
    }
}