using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using TourPlannerDL;
using System.Data;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private httpBusiness http = new();
        private DBBusiness db = new();
        private MediaItem currentTour;
        private MediaFolder folder;
        private string fromDest;
        private string toDest;
        private string addReport;
        private string addRating;
        private string addAnimals;
        private string addCost;
        private string addWeather;
        private string addTime;
        private string addDate;
        private string addDistance;
        private string addHighway;
        private string addAccess;
        private DataTable logDataTable;

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
        public ICommand AddLog { get; set; }
        public ObservableCollection<MediaItem> Tours { get; set; }
        public ObservableCollection<Logs> Logs { get; set; }
        public ObservableCollection<Logs> AddLogs { get; set; }
        public ObservableCollection<Description> Description { get; set; }

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
        public string AddReport
        {
            get { return addReport; }
            set
            {
                if ((addReport != value))
                {
                    addReport = value;
                    RaisePropertyChangedEvent(nameof(AddReport));
                }
            }
        }
        public string AddRating
        {
            get { return addRating; }
            set
            {
                if ((addRating != value))
                {
                    addRating = value;
                    RaisePropertyChangedEvent(nameof(AddRating));
                }
            }
        }
        public string AddAnimals
        {
            get { return addAnimals; }
            set
            {
                if ((addAnimals != value))
                {
                    addAnimals = value;
                    RaisePropertyChangedEvent(nameof(AddAnimals));
                }
            }
        }
        public string AddCost
        {
            get { return addCost; }
            set
            {
                if ((addCost != value))
                {
                    addCost = value;
                    RaisePropertyChangedEvent(nameof(AddCost));
                }
            }
        }
        public string AddWeather
        {
            get { return addWeather; }
            set
            {
                if ((addWeather != value))
                {
                    addWeather = value;
                    RaisePropertyChangedEvent(nameof(AddWeather));
                }
            }
        }
        public string AddTime
        {
            get { return addTime; }
            set
            {
                if ((addTime != value))
                {
                    addTime = value;
                    RaisePropertyChangedEvent(nameof(AddTime));
                }
            }
        }
        public string AddDate
        {
            get { return addDate; }
            set
            {
                if ((addDate != value))
                {
                    addDate = value;
                    RaisePropertyChangedEvent(nameof(AddDate));
                }
            }
        }
        public string AddDistance
        {
            get { return addDistance; }
            set
            {
                if ((addDistance != value))
                {
                    addDistance = value;
                    RaisePropertyChangedEvent(nameof(AddDistance));
                }
            }
        }
        public string AddHighway
        {
            get { return addHighway; }
            set
            {
                if ((addHighway != value))
                {
                    addHighway = value;
                    RaisePropertyChangedEvent(nameof(AddHighway));
                }
            }
        }
        public string AddAccess
        {
            get { return addAccess; }
            set
            {
                if ((addAccess != value))
                {
                    addAccess = value;
                    RaisePropertyChangedEvent(nameof(AddAccess));
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
                    RaisePropertyChangedEvent(nameof(Description));
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
        public DataTable LogDataTable
        {
            get { return logDataTable; }
            set
            {
                if ((logDataTable != value))
                {
                    logDataTable = value;
                    RaisePropertyChangedEvent(nameof(LogDataTable));
                }
            }
        }

        public FolderViewModel()
        {
            this.mediaManager = TourPlannerManagerFactory.GetFactoryManager();
            Tours = new ObservableCollection<MediaItem>();
            Description = new ObservableCollection<Description>();
            Logs = new ObservableCollection<Logs>();
            folder = mediaManager.GetMediaFolder("Get Media Folder From Disk");
            this.AddRoute = new RelayCommand(o => {
                db.InsertNewRoute(FromDest, ToDest);
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
                http.FindRoute("Wien", "London");
                FillListViewDescription(CurrentTour.Name);
                //FillLogs(CurrentTour.Name);
            });
            this.DeleteRoute = new RelayCommand(o => {
                db.DeleteRoute(CurrentTour.Name);
                Tours.Remove(CurrentTour);
            });
            this.ZoomInCommand = new RelayCommand((_) => Scale += ScaleStep, (_) => Scale < MaximumScale);
            this.ZoomOutCommand = new RelayCommand((_) => Scale -= ScaleStep, (_) => Scale > MinimumScale);
            this.ResetZoomCommand = new RelayCommand((_) => Scale = Unity, (_) => Scale != Unity);
            this.AddLog = new RelayCommand(o => {
                FillAddLogs();
            });
            InitListViewTour();
        }

        public void InitListViewTour()
        {
            FillListViewTours();
        }

        private void FillListViewTours()
        {
            foreach (MediaItem item in mediaManager.GetTours(folder))
            {
                Tours.Add(item);
            }
        }
        public void InitListViewDescription(string Name)
        {
            FillListViewDescription(Name);
        }

        private void FillListViewDescription(string Name)
        {
            foreach (Description description in mediaManager.GetDescription(Name))
            {
                Description.Add(description);
            }
        }

        private void FillLogs(string Name)
        {
            foreach(Logs item in db.GetLogs(Name))
            {
                Logs.Add(item);
            }
        }

        private void FillAddLogs()
        {
            AddLogs = new ObservableCollection<Logs>();
            AddLogs.Add(new Logs() {
                Report = AddReport,
                Weather = AddWeather,
                Time = AddTime,
                Date = AddDate,
                Highway = AddHighway,
                Distance = AddDistance,
                Access = AddAccess,
                Rating = AddRating,
                Animals = AddAnimals,
                Cost = AddCost
            });
            db.InsertLog(AddLogs);
        }
    }
}
