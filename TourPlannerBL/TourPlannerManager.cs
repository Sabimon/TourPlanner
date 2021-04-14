using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlannerBL {
    public interface TourPlannerManager {
        MediaFolder GetMediaFolder(string url);
        IEnumerable<MediaItem> GetInfos(MediaFolder folder);
        IEnumerable<MediaItem> GetTours(MediaFolder folder);
        IEnumerable<MediaItem> SearchForInfos(string itemName, MediaFolder folder, bool caseSensitive = false);
        IEnumerable<MediaItem> SearchForTours(string tourName, MediaFolder folder, bool caseSensitive = false);
    }
}
