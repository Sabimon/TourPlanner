using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using TourPlanner.ViewModels;
using TourPlannerBL;
using TourPlannerModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourManagerTest
    {
        private TourPlannerManager GetTourPlannerManager()
        {
            var mock = new Mock<TourPlannerManager>();
            return mock.Object;
        }
        //private TourPlannerManager testManager;
        private ObservableCollection<Tour> TestTours { get; set; }
        private ObservableCollection<Tour> AssertTours { get; set; }

        [Test]
        public void GetToursNotNull()
        {
            TourPlannerManager testManager= TourPlannerManagerFactory.GetFactoryManager();
            //TourPlannerManager testManager = GetTourPlannerManager();
            AssertTours = new ObservableCollection<Tour>();
            AssertTours =testManager.GetTours(AssertTours);
            Assert.NotNull(AssertTours);
        }
        [Test]
        public void TextSearchTestWithResult()
        {
            TourPlannerManager testManager = TourPlannerManagerFactory.GetFactoryManager();
            TestTours = new ObservableCollection<Tour>();
            AssertTours = new ObservableCollection<Tour>();
            TestTours.Add(new Tour()
            {
                Name = "TestTour"
            });
            string TestString = "Test";
            AssertTours = testManager.SearchForTours(TestString, TestTours);
            Assert.AreEqual(AssertTours, TestTours);
        }
        [Test]
        public void TextSearchTestWithoutResult()
        {
            TourPlannerManager testManager = TourPlannerManagerFactory.GetFactoryManager();
            TestTours = new();
            AssertTours = new();
            TestTours.Add(new Tour()
            {
                Name = "TestTour"
            });
            string TestString = "abc";
            AssertTours=testManager.SearchForTours(TestString, TestTours);
            Assert.AreNotEqual(AssertTours, TestTours);
        }
    }
}
