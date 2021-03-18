using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;

namespace TourPlannerBL {
    internal class TourPlannerManagerImpl : TourPlannerManager {

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            // usually querying the disk, or from a DB, or ...
            return new List<MediaItem>() {
                new MediaItem() { Name = "Tour1" },
                new MediaItem() { Name = "Tour2" },
                new MediaItem() { Name = "Tour3" }
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
    }
}
