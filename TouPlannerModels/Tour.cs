using System.Collections.ObjectModel;

namespace TourPlannerModels 
{
    public class Tour {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int TourID { get; set; }
        public ObservableCollection<Logs> Logs { get; set; }
        public ObservableCollection<Description> Description { get; set; }
    }
}
