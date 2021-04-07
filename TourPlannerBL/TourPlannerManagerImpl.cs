using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;
using TourPlannerDL;

namespace TourPlannerBL {
    internal class TourPlannerManagerImpl : TourPlannerManager {

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            DBOutput db = new DBOutput();
            return db.GetRoutes(folder);
        }

        public IEnumerable<MediaTour> GetTours(MediaFolder folder2){
            // usually querying the disk, or from a DB, or ...
            return new List<MediaTour>() {
                new MediaTour() { Name = "dauert lange" }
            };
        }

        public MediaFolder GetMediaFolder(string url) {
            // usally located somewhere on the disk
            return new MediaFolder();
        }

        public IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false) {
            IEnumerable<MediaItem> items = GetItems(folder);

            if (caseSensitive) {
                return items.Where(x => x.Name.Contains(itemName)); //sucht nach items & lambda ist auch da
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }
        public IEnumerable<MediaTour> SearchForTours(string tourName, MediaFolder folder2, bool caseSensitive = false){
            IEnumerable<MediaTour> tours = GetTours(folder2);

            if (caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(tourName)); //sucht nach items & lambda ist auch da
            }
            return tours.Where(x => x.Name.ToLower().Contains(tourName.ToLower()));
        }
    }
}
