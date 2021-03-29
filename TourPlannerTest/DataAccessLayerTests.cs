using NUnit.Framework;
using TourPlannerDL;

namespace TourPlannerTest
{
    public class Tests
    {
        httpListener http = new httpListener();

        [Test]
        public void httpListenerNotNull()
        {
            Assert.NotNull(http);
        }
    }
}