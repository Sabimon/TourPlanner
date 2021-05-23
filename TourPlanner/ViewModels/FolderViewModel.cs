using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerBL;
using TourPlannerModels;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Data;
using log4net;

namespace TourPlanner.ViewModels
{
    public class FolderViewModel : ViewModelBase
    {
        private TourPlannerManager tourManager;
        private httpBusiness http = new();
        private DBBusiness db = new();
        private StringHandler strHandler = new();
        private ReportHandler reportHandler = new();
        private JsonHandler jsonHandler = new();
        private Tour currentTour;
        private MediaFolder folder;
        private string searchString;
        private string fromDest;
        private string toDest;
        private string report;
        private string weather;
        private DataTable logDataTable;
        private static readonly ILog log = LogManager.GetLogger(typeof(FolderViewModel));

        private const decimal Unity = 1;
        private decimal _scale = Unity;
        public decimal ScaleStep => 0.1m;
        public decimal MinimumScale => 0.1m;
        public decimal MaximumScale => 4.0m;
        //Code for zoom commands snacked from: https://www.carlosjanderson.com/let-users-zoom-in-or-out-of-a-wpf-view/

        public ICommand AddRoute { get; set; }
        public ICommand ChangeRoute { get; set; }
        public ICommand DeleteRoute { get; set; }
        public ICommand TextSearch { get; set; }
        public ICommand ResetSearch { get; set; }
        public ICommand ZoomOutCommand { get; set; }
        public ICommand ZoomInCommand { get; set; }
        public ICommand ResetZoomCommand { get; set; }
        public ICommand AddLog { get; set; }
        public ICommand ChangeLog { get; set; }
        public ICommand DeleteLog { get; set; }
        public ICommand PrintCurrentTourAsPDF { get; set; }
        public ICommand PrintAllToursAsPDF { get; set; }
        public ICommand ImportTour { get; set; }
        public ICommand ExportTour { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Tour> ResultTours { get; set; }
        public ObservableCollection<Logs> Logs { get; set; }
        public ObservableCollection<Logs> AddLogs { get; set; }
        public ObservableCollection<Logs> ChangeLogs { get; set; }
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
        public string SearchString
        {
            get { return searchString; }
            set
            {
                if (searchString != value && strHandler.StringValidation(value) == true)
                {
                    searchString = value;
                    RaisePropertyChangedEvent(nameof(SearchString));
                }
            }
        }
        public string FromDest
        {
            get { return fromDest; }
            set
            {
                if (fromDest != value && strHandler.StringValidation(value) == true)
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
                if (toDest != value && strHandler.StringValidation(value) == true)
                {
                    toDest = value;
                    RaisePropertyChangedEvent(nameof(ToDest));
                }
            }
        }
        public string ReportProperty
        {
            get { return report; }
            set
            {
                if (report != value && strHandler.StringValidation(value) ==true)
                {
                    report = value;
                    RaisePropertyChangedEvent(nameof(ReportProperty));
                }
            }
        }
        public string WeatherProperty
        {
            get { return weather; }
            set
            {
                if (weather != value && strHandler.StringValidation(value) == true)
                {
                    weather = value;
                    RaisePropertyChangedEvent(nameof(WeatherProperty));
                }
            }
        }
        public int RatingProperty { get; set; }
        public bool AnimalsProperty { get; set; }
        public decimal CostProperty { get; set; }
        public decimal TimeProperty { get; set; }
        public DateTime DateProperty { get; set; }
        public DateTime ChangeDateProperty { get; set; }
        public decimal DistanceProperty { get; set; }
        public bool HighwayProperty { get; set; }
        public bool AccessProperty { get; set; }
        public string ChangeID { get; set; }
        public string DeleteID { get; set; }

        public Tour CurrentTour
        {
            get { return currentTour; }
            set
            {
                if ((currentTour != value) && (value != null))
                {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    RaisePropertyChangedEvent(nameof(Logs));
                    RaisePropertyChangedEvent(nameof(SelectedTourMapImage));
                    UpdateLogs(CurrentTour);
                    UpdateDescription();
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
            this.tourManager = TourPlannerManagerFactory.GetFactoryManager();
            Tours = new ObservableCollection<Tour>();
            ResultTours = new ObservableCollection<Tour>();
            Logs = new ObservableCollection<Logs>();
            Description = new ObservableCollection<Description>();
            folder = tourManager.GetMediaFolder();
            this.AddRoute = new RelayCommand(async o =>
            {
                db.InsertNewRoute(FromDest, ToDest);
                string Name = $"{FromDest}-{ToDest}";
                http.FindRoute(Name);
                await http.GetAndSaveImage(Name);
                Tours.Clear();
                FillListViewTours();//update Item List
            }, (_) =>
            { //(_) braucht keinen Parameter
                if (FromDest != null && FromDest.Length > 0 && ToDest != null && ToDest.Length > 0)
                {
                    return true;
                }
                return false;
            });
            this.ChangeRoute = new RelayCommand(o =>
            {
                //not done yet
            });
            this.DeleteRoute = new RelayCommand(o =>
            {
                db.DeleteRoute(CurrentTour.Name);
                Tours.Remove(CurrentTour);
            });
            this.TextSearch = new RelayCommand(o =>
            {
                ResultTours = tourManager.SearchForTours(SearchString);
                FillTourListWithSearchResult(ResultTours);
            });
            this.ResetSearch = new RelayCommand(o =>
            {
                InitListViewTour();
            });
            this.ZoomInCommand = new RelayCommand((_) => Scale += ScaleStep, (_) => Scale < MaximumScale);
            this.ZoomOutCommand = new RelayCommand((_) => Scale -= ScaleStep, (_) => Scale > MinimumScale);
            this.ResetZoomCommand = new RelayCommand((_) => Scale = Unity, (_) => Scale != Unity);
            this.AddLog = new RelayCommand(o =>
            {
                AddLogDB(CurrentTour.TourID);
                UpdateLogs(CurrentTour);
            });
            this.ChangeLog = new RelayCommand(o =>
            {
                ChangeLogDB(ChangeID);
                UpdateLogs(CurrentTour);
            });
            this.DeleteLog = new RelayCommand(o =>
            {
                DeleteLogDB(DeleteID);
                UpdateLogs(CurrentTour);
            });
            this.PrintCurrentTourAsPDF = new RelayCommand(o =>
            {
                PrintOneTour(CurrentTour);
            });
            this.PrintAllToursAsPDF = new RelayCommand(o =>
            {
                PrintAllTours();
            });
            this.ImportTour = new RelayCommand(o =>
            {
                CurrentTour=jsonHandler.ImportTour(CurrentTour);
                UpdateLogs(CurrentTour);
                UpdateDescription();
            });
            this.ExportTour = new RelayCommand(o =>
            {
                jsonHandler.ExportTour(CurrentTour);
            });
            InitListViewTour();
        }

        public void InitListViewTour()
        {
            FillListViewTours();
        }

        private void FillListViewTours()
        {
            Tours.Clear();
            Tours = tourManager.GetTours(Tours);
            log.Info("Fill ListView with Tours");
        }
        public void InitListViewDescription(Tour CurrentTour)
        {
            FillListViewDescription(CurrentTour);
        }
        private void FillTourListWithSearchResult(ObservableCollection<Tour> ResultTours)
        {
            Tours.Clear();
            foreach (Tour tour in ResultTours)
            {
                Tours.Add(tour);
            }
        }
        private void FillListViewDescription(Tour CurrentTour)
        {
            Description=tourManager.GetDescription(Description, CurrentTour.TourID);
            CurrentTour.Description = Description;
            log.Info("Fill ListView with Description");
        }

        private void FillLogs(Tour CurrentTour)
        {
            Logs=db.GetLogs(Logs, CurrentTour.TourID);
            CurrentTour.Logs = Logs;
            log.Info("Fill ListView with Logs");
        }

        private void AddLogDB(int ID)
        {
            AddLogs = new ObservableCollection<Logs>();
            AddLogs.Add(new Logs()
            {
                Report = ReportProperty,
                Weather = WeatherProperty,
                Time = TimeProperty.ToString(),
                Date = DateProperty.ToString(),
                Highway = HighwayProperty.ToString(),
                Distance = DistanceProperty.ToString(),
                Access = AccessProperty.ToString(),
                Rating = RatingProperty.ToString(),
                Animals = AnimalsProperty.ToString(),
                Cost = CostProperty.ToString()
            });
            db.InsertLog(AddLogs, ID);
            log.Info("Add Logs to Route");
        }
        private void ChangeLogDB(string ChangeID)
        {
            ChangeLogs = new ObservableCollection<Logs>();
            ChangeLogs.Add(new Logs()
            {
                Report = ReportProperty,
                Weather = WeatherProperty,
                Time = TimeProperty.ToString(),
                Date = ChangeDateProperty.ToString(),
                Highway = HighwayProperty.ToString(),
                Distance = DistanceProperty.ToString(),
                Access = AccessProperty.ToString(),
                Rating = RatingProperty.ToString(),
                Animals = AnimalsProperty.ToString(),
                Cost = CostProperty.ToString()
            });
            db.ChangeLog(ChangeLogs, ChangeID);
            log.Info("Change Log from Route");
        }
        private void DeleteLogDB(string DeleteID)
        {
            db.DeleteLog(DeleteID);
            log.Info("Delete Log from Route");
        }
        private void UpdateLogs(Tour CurrentTour)
        {
            Logs.Clear();
            FillLogs(CurrentTour);
        }
        private void UpdateDescription()
        {
            Description.Clear();
            FillListViewDescription(CurrentTour);
        }
        private void PrintOneTour(Tour CurrentTour)
        {
            reportHandler.PrintOneTour(CurrentTour);
        }
        private void PrintAllTours()
        {
            reportHandler.PrintTourSummary(Tours);
        }
    }
}
