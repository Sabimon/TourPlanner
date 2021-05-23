using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlannerModels;

namespace TourPlannerBL {
    public interface TourPlannerManager {
        MediaFolder GetMediaFolder();
        ObservableCollection<Description> GetDescription(ObservableCollection<Description> Description, int ID);
        ObservableCollection<Tour> GetTours(ObservableCollection<Tour> AllTours);
        ObservableCollection<Tour> SearchForTours(string tourName, ObservableCollection<Tour> AllTours);
    }
}
