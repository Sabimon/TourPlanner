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

        /*public IEnumerable<Tour> SearchForTours(string itemName, MediaFolder folder, bool caseSensitive = false) {
            IEnumerable<Tour> items = GetTours();

            if (caseSensitive) {
                return items.Where(x => x.Name.Contains(itemName)); //sucht nach items & lambda ist auch da
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }*/
    }
}
