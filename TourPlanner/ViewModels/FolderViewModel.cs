using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private MediaItem currentItem;
        private MediaTour currentTour;
        private MediaFolder folder;
        private string fromDest;
        private string toDest;
        private string searchTour;
        public string imgPath=@"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\Wien-Linz.jpg";

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand SearchRoute { get; set; }
        public ObservableCollection<MediaItem> Items { get; set; }
        public ObservableCollection<MediaTour> Tours { get; set; }

        public string FromDest
        {
            get { return fromDest; }
            set
            {
                if ((fromDest != value))
                {
                    fromDest = value;
                    RaisePropertyChangedEvent(nameof(FromDest)); 
                }
            }
        }
        public string ToDest
        {
            get { return toDest; }
            set
            {
                if ((toDest != value))
                {
                    toDest = value;
                    RaisePropertyChangedEvent(nameof(ToDest));
                }
            }
        }
        public string SearchTour
        {
            get { return searchTour; }
            set
            {
                if ((searchTour != value))
                {
                    searchTour = value;
                    RaisePropertyChangedEvent(nameof(SearchTour));
                }
            }
        }

        public MediaTour CurrentTour
        {
            get { return currentTour; }
            set
            {
                if ((currentTour != value) && (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour)); 
                }
            }
        }
        public MediaItem CurrentItem
        {
            get { return currentItem; }
            set
            {
                if ((currentItem != value) && (value != null))
                {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }
            }
        }
        public string ImagePath
        {
            get { return imgPath; }
            set
            {
                if ((imgPath != value))
                {
                    imgPath = value;
                    RaisePropertyChangedEvent(nameof(ImagePath));
                }
            }
        }

        public FolderViewModel()
        {
            this.mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            httpListener http = new httpListener();
            Items = new ObservableCollection<MediaItem>();
            Tours = new ObservableCollection<MediaTour>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            this.SearchRoute = new RelayCommand(o => {
                //http.FindRoute(FromDest, ToDest);
                //http.GetAndSaveImage(FromDest, ToDest);
            }, (_) =>{ //(_) braucht keinen Parameter
                if (FromDest != null && FromDest.Length > 0 && ToDest != null && ToDest.Length > 0)
                {
                    return true;
                }
                return false;
            }
            );
            InitListView();
            InitListViewTour();
        }


        public void InitListView()
        {
            Items = new ObservableCollection<MediaItem>();
            FillListView();
        }

        public void InitListViewTour()
        {
            Tours = new ObservableCollection<MediaTour>();
            FillListViewTours();
        }

        private void FillListView()
        {
            foreach (MediaItem item in mediaManager.GetItems(folder))
            {
                Items.Add(item);
            }
        }
        private void FillListViewTours()
        {
            foreach (MediaTour tour in mediaManager.GetTours(folder))
            {
                Tours.Add(tour);
            }
        }
    }
}
