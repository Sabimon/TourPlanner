using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private MediaItem currentItem;
        private MediaTour currentTour;
        private MediaFolder folder;
        private string searchName;
        private string searchTour;

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ObservableCollection<MediaItem> Items { get; set; }
        public ObservableCollection<MediaTour> Tours { get; set; }

        public string SearchName
        {
            get { return searchName; }
            set
            {
                if ((searchName != value))
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName)); 
                }
            }
        }

        public string SearchTour{
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


        public FolderViewModel()
        {
            this.mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            Items = new ObservableCollection<MediaItem>();
            Tours = new ObservableCollection<MediaTour>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            this.SearchCommand = new RelayCommand(o => {
                IEnumerable<MediaItem> items = mediaManager.SearchForItems(SearchName, folder);
                Items.Clear();
                foreach (MediaItem item in items)
                {
                    Items.Add(item);
                }
            }, (_) =>{ //(_) braucht keinen Parameter
                if (SearchName != null && SearchName.Length > 0)
                {
                    return true;
                }
                return false;
            }
            );

            this.ClearCommand = new RelayCommand(o => {
                Items.Clear();
                SearchName = "";

                FillListView();
            });
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
