using System.Collections.Generic;
using System.Linq;
using TourPlannerModels;

namespace TourPlannerBL {
    internal class TourPlannerManagerImpl : TourPlannerManager {

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            // usually querying the disk, or from a DB, or ...
            return new List<MediaItem>() {
                new MediaItem() { Name = "Item1" },
                new MediaItem() { Name = "Item2" },
                new MediaItem() { Name = "Another" },
                new MediaItem() { Name = "SWEI" },
                new MediaItem() { Name = "FHTW" }
            };
        }

        public MediaFolder GetMediaFolder(string url) {
            // usally located somewhere on the disk
            return new MediaFolder();
        }

        public IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false) {
            IEnumerable<MediaItem> items = GetItems(folder);

            if (caseSensitive) {
                return items.Where(x => x.Name.Contains(itemName));
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }
    }
}
