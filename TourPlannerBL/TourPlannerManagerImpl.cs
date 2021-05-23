using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;
using TourPlannerDL;
using System.Collections.ObjectModel;

namespace TourPlannerBL {
    internal class TourPlannerManagerImpl : TourPlannerManager {
        DBOutput db = new DBOutput();
        public ObservableCollection<Tour> GetTours(ObservableCollection<Tour> AllTours) {
            foreach (Tour tour in db.GetRoutes())
            {
                AllTours.Add(tour);
            }
            return AllTours;
        }

        public ObservableCollection<Description> GetDescription(ObservableCollection<Description> Description, int ID)
        {
            foreach (Description description in db.GetDescription(ID))
            {
                Description.Add(description);
            }
            return Description;
        }

        public MediaFolder GetMediaFolder() {
            // usally located somewhere on the disk
            return new MediaFolder();
        }

        public ObservableCollection<Tour> SearchForTours(string tourName) {
            ObservableCollection<Tour> AllTours = new();
            AllTours = GetTours(AllTours);
            ObservableCollection<Tour> ResultTours = new();
            foreach (Tour tour in AllTours)
            {
                if (tour.Name.ToLower().Contains(tourName.ToLower()))
                {
                    ResultTours.Add(tour);
                }
            }
            return ResultTours;
        }
    }
}
