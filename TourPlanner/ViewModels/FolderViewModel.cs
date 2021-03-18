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
        private MediaFolder folder;
        private string searchName="test";

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ObservableCollection<MediaItem> Items { get; set; }

        public string SearchName
        {
            get { return searchName; }
            set
            {
                if ((searchName != value))
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName)); //verteh ich nicht
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
                    RaisePropertyChangedEvent(nameof(CurrentItem)); //verteh ich nicht
                }
            }
        }

        public FolderViewModel()
        {
            this.mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            Items = new ObservableCollection<MediaItem>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            //lol lambda expressions check gar nicht was abgeht
            this.SearchCommand = new RelayCommand(o => {
                IEnumerable<MediaItem> items = mediaManager.SearchForItems(SearchName, folder);
                Items.Clear();
                foreach (MediaItem item in items)
                {
                    Items.Add(item);
                }
            });

            this.ClearCommand = new RelayCommand(o => {
                Items.Clear();
                SearchName = "";

                FillListView();
            });

            InitListView();
        }


        public void InitListView()
        {
            Items = new ObservableCollection<MediaItem>();
            FillListView();
        }

        private void FillListView()
        {
            foreach (MediaItem item in mediaManager.GetItems(folder))
            {
                Items.Add(item);
            }
        }

    }
}
