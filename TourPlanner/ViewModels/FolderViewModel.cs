using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;
using System.Windows.Media;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private httpListener http = httpListener.Instance();
        //private DBInput dbIn=new(); 
        private MediaItem currentItem;
        private MediaTour currentTour;
        private MediaFolder folder;
        private string fromDest;
        private string toDest;
        private string searchTour;
        public string imgPath= @"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\Wien-Linz.jpg";

        private const decimal Unity = 1;
        private decimal _scale = Unity;
        public decimal ScaleStep => 0.1m;
        public decimal MinimumScale => 0.1m;
        public decimal MaximumScale => 4.0m;

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand AddRoute { get; set; }
        public ICommand SearchRoute { get; set; }
        public ICommand DeleteRoute { get; set; }
        public ICommand ZoomOutCommand { get; set; }
        public ICommand ZoomInCommand { get; set; }
        public ICommand ResetZoomCommand { get; set; }
        public ObservableCollection<MediaItem> Items { get; set; }
        public ObservableCollection<MediaTour> Tours { get; set; }

        public decimal Scale
        {
            get { return _scale; }
            set
            {
                if ((_scale != value))
                {
                    _scale = value;
                    RaisePropertyChangedEvent(nameof(Scale));
                }
            }
        }
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
            Items = new ObservableCollection<MediaItem>();
            Tours = new ObservableCollection<MediaTour>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            this.AddRoute = new RelayCommand(o => {
                //dbIn.InsertNewRoute(FromDest, ToDest);
                Items.Clear();
                FillListView();//update Item List, there is propably a better way to do this
            }, (_) =>{ //(_) braucht keinen Parameter
                if (FromDest != null && FromDest.Length > 0 && ToDest != null && ToDest.Length > 0)
                {
                    return true;
                }
                return false;
            });
            this.SearchRoute = new RelayCommand(o => {
                //http.FindRoute(from - to substrings);
            });
            this.DeleteRoute = new RelayCommand(o => {
                //dbIn.DeleteRoute(CurrentItem.Name);
                Items.Remove(CurrentItem);
            });
            this.ZoomInCommand = new RelayCommand((_) => Scale += ScaleStep, (_) => Scale < MaximumScale);
            this.ZoomOutCommand = new RelayCommand((_) => Scale -= ScaleStep, (_) => Scale > MinimumScale);
            this.ResetZoomCommand = new RelayCommand((_) => Scale = Unity, (_) => Scale != Unity);
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
