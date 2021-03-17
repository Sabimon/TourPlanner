using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerBL {
    public interface TourPlannerManager {
        MediaFolder GetMediaFolder(string url);
        IEnumerable<MediaItem> GetItems(MediaFolder folder);
        IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false);
    }
}
