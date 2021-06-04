using NUnit.Framework;
using TourPlanner.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class ViewModelTests
    {
        private FolderViewModel VM = new();
        [Test]
        public void ToursNotNull()
        {
            Assert.NotNull(VM.Tours);
        }
        [Test]
        public void PropertyTest()
        {
            VM.FromDest = "Wien";
            Assert.AreEqual(VM.FromDest, "Wien");
        }
        [Test]
        public void ZoomInCommandTest()
        {
            decimal TestDecimal = 1.1m;
            VM.ZoomInCommand.Execute(TestDecimal);
            Assert.AreEqual(VM.Scale, TestDecimal);
        }
        [Test]
        public void ZoomResetCommandTest()
        {
            decimal TestDecimal = 1.0m;
            VM.ResetZoomCommand.Execute(TestDecimal);
            Assert.AreEqual(VM.Scale, TestDecimal);
        }
        [Test]
        public void ZoomOutCommandTest()
        {
            decimal TestDecimal = 0.9m;
            VM.ResetZoomCommand.Execute(TestDecimal);
            VM.ZoomOutCommand.Execute(TestDecimal);
            Assert.AreEqual(VM.Scale, TestDecimal);
        }
    }
}