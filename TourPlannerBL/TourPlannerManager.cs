using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerBL {
    public interface TourPlannerManager {
        MediaFolder GetMediaFolder(string url);
        ObservableCollection<Description> GetDescription(string Name);
        IEnumerable<Tour> GetTours(MediaFolder folder);
        //IEnumerable<MediaItem> SearchForDescription(string itemName, MediaFolder folder, bool caseSensitive = false);
        IEnumerable<Tour> SearchForTours(string tourName, MediaFolder folder, bool caseSensitive = false);
    }
}
