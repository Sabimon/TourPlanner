using NUnit.Framework;
using System.Collections.ObjectModel;
using TourPlannerDL;
using TourPlannerModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class PDFOutputTest
    {
        [Test]
        public void PDFOutputNotNull()
        {
            PDFOutput Print = new PDFOutput();
            Assert.NotNull(Print);
        }
        [Test]
        public void CreateTableNotNullTest()
        {
            PDFOutput Print = new PDFOutput();
            ObservableCollection<Logs> Logs = new();
            Logs.Add(new Logs()
            {
                Report = "test",
                Weather = "test",
                Time = "test",
                Date = "test",
                Highway = "test",
                Distance = "test",
                Access = "test",
                Rating = "test",
                Animals = "test",
                Cost = "test"
            });
            Assert.NotNull(Print.CreateLogTable(Logs, 10));
        }
        [Test]
        public void CreateTableSizeTest()
        {
            PDFOutput Print = new();
            ObservableCollection<Logs> Logs = new();
            Logs.Add(new Logs()
            {
                Report = "test",
                Weather = "test",
                Time = "test",
                Date = "test",
                Highway = "test",
                Distance = "test",
                Access = "test",
                Rating = "test",
                Animals = "test",
                Cost = "test"
            });
            Assert.AreEqual(2, (Print.CreateLogTable(Logs, 10)).GetNumberOfRows());
        }
    }
}
