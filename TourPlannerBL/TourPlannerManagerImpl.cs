using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;
using TourPlannerDL;
using System.Collections.ObjectModel;

namespace TourPlannerBL {
    internal class TourPlannerManagerImpl : TourPlannerManager {
        DBOutput db = new DBOutput();
        public IEnumerable<Tour> GetTours(MediaFolder folder) {
            return db.GetRoutes(folder);
        }

        public ObservableCollection<Description> GetDescription(string Name)
        {
            int ID;
            ID = db.GetRouteID(Name);
            return db.GetDescription(ID);
        }

        public MediaFolder GetMediaFolder(string url) {
            // usally located somewhere on the disk
            return new MediaFolder();
        }

        public IEnumerable<Tour> SearchForTours(string itemName, MediaFolder folder, bool caseSensitive = false) {
            IEnumerable<Tour> items = GetTours(folder);

            if (caseSensitive) {
                return items.Where(x => x.Name.Contains(itemName)); //sucht nach items & lambda ist auch da
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }
        /*public IEnumerable<MediaItem> SearchForDescription(string tourName, MediaFolder folder2, bool caseSensitive = false){
            ObservableCollection<Description> tours = GetDescription();

            if (caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(tourName)); //sucht nach items & lambda ist auch da
            }
            return tours.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }*/
    }
}
