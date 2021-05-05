using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerBL {
    public interface TourPlannerManager {
        MediaFolder GetMediaFolder(string url);
        ObservableCollection<Description> GetDescription(string Name);
        IEnumerable<MediaItem> GetTours(MediaFolder folder);
        //IEnumerable<MediaItem> SearchForDescription(string itemName, MediaFolder folder, bool caseSensitive = false);
        IEnumerable<MediaItem> SearchForTours(string tourName, MediaFolder folder, bool caseSensitive = false);
    }
}
