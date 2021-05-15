using NUnit.Framework;
using TourPlanner.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class ViewModelTests
    {
        [Test]
        public void PropertyTest()
        {
            FolderViewModel VM = new FolderViewModel();
            Assert.NotNull(VM.Tours);
        }
    }
}
