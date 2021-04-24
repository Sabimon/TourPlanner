using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;
using TourPlannerDL;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private httpListener http = httpListener.Instance();
        private DBInput dbIn=new();
        private MediaItem currentTour;
        private MediaItem currentInfo;
        private MediaFolder folder;
        private string fromDest;
        private string toDest;

        private const decimal Unity = 1;
        private decimal _scale = Unity;
        public decimal ScaleStep => 0.1m;
        public decimal MinimumScale => 0.1m;
        public decimal MaximumScale => 4.0m;
        //Code for zoom commands snacked from: https://www.carlosjanderson.com/let-users-zoom-in-or-out-of-a-wpf-view/

        public ICommand AddRoute { get; set; }
        public ICommand SearchRoute { get; set; }
        public ICommand DeleteRoute { get; set; }
        public ICommand ZoomOutCommand { get; set; }
        public ICommand ZoomInCommand { get; set; }
        public ICommand ResetZoomCommand { get; set; }
        public ObservableCollection<MediaItem> Tours { get; set; }
        public ObservableCollection<MediaItem> Infos { get; set; }

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

        public MediaItem CurrentTour
        {
            get { return currentTour; }
            set
            {
                if ((currentTour != value) && (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    RaisePropertyChangedEvent(nameof(SelectedTourMapImage));
                }
            }
        }
        public MediaItem CurrentInfo
        {
            get { return currentInfo; }
            set
            {
                if ((currentInfo != value) && (value != null))
                {
                    currentInfo = value;
                    RaisePropertyChangedEvent(nameof(CurrentInfo));
                }
            }
        }

        public ImageSource SelectedTourMapImage
        {
            get
            {
                if (CurrentTour != null)
                {
                    string location = $@"C:\Users\Lenovo\source\repos\TourPlanner\TourPlannerDL\MapResponses\{CurrentTour.Name}.jpg";
                    if (File.Exists(location))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                        bitmap.UriSource = new Uri(location);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        return bitmap;
                    }
                }
                return null;
            }
        }

        public FolderViewModel()
        {
            this.mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            Tours = new ObservableCollection<MediaItem>();
            Infos = new ObservableCollection<MediaItem>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            this.AddRoute = new RelayCommand(o => {
                dbIn.InsertNewRoute(FromDest, ToDest);//in BL
                Tours.Clear();
                FillListViewTours();//update Item List
            }, (_) =>{ //(_) braucht keinen Parameter
                if (FromDest != null && FromDest.Length > 0 && ToDest != null && ToDest.Length > 0)
                {
                    return true;
                }
                return false;
            });
            this.SearchRoute = new RelayCommand(o => {
                JsonHandler json = new();
                json.DeserializeJSON(); //in BL
                //http.FindRoute(from - to substrings);
                //http.FindRoute("Wien", "London");
            });
            this.DeleteRoute = new RelayCommand(o => {
                dbIn.DeleteRoute(CurrentTour.Name); //in BL
                Tours.Remove(CurrentTour);
            });
            this.ZoomInCommand = new RelayCommand((_) => Scale += ScaleStep, (_) => Scale < MaximumScale);
            this.ZoomOutCommand = new RelayCommand((_) => Scale -= ScaleStep, (_) => Scale > MinimumScale);
            this.ResetZoomCommand = new RelayCommand((_) => Scale = Unity, (_) => Scale != Unity);
            InitListViewInfos();
            InitListViewTour();
        }

        public void InitListViewTour()
        {
            Tours = new ObservableCollection<MediaItem>();
            FillListViewTours();
        }

        private void FillListViewTours()
        {
            foreach (MediaItem item in mediaManager.GetTours(folder))
            {
                Tours.Add(item);
            }
        }
        public void InitListViewInfos()
        {
            Infos = new ObservableCollection<MediaItem>();
            FillListViewInfos();
        }

        private void FillListViewInfos()
        {
            foreach (MediaItem tour in mediaManager.GetInfos(folder))
            {
                Infos.Add(tour);
            }
        }
    }
}
