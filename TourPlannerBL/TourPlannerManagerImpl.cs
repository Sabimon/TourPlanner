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
            if (AllTours == null)
            {
                AllTours.Add(new Tour()
                {
                    Name="Tours empty"
                });
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

        public ObservableCollection<Tour> SearchForTours(string Input, ObservableCollection<Tour> AllTours) {
            ObservableCollection<Tour> ResultTours = new();
            ObservableCollection<Logs> Logs = new();
            foreach (Tour tour in AllTours)
            {
                if (tour.Name.ToLower().Contains(Input.ToLower()))
                {
                    ResultTours.Add(tour);
                }
                Logs = db.GetLogs(tour.TourID);
                foreach(Logs log in Logs)
                {
                    if (log.Report.ToLower().Contains(Input.ToLower())
                        || log.Weather.ToLower().Contains(Input.ToLower()))
                    {
                        ResultTours.Add(tour);
                    }
                }
            }
            if (ResultTours == null)
            {
                ResultTours.Add(new Tour()
                {
                    Name = "No Tour found"
                });
            }
            return ResultTours;
        }
    }
}
