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
    public class Properties : ViewModelBase
    {
        private TourPlannerManager mediaManager;
        private httpBusiness http = new();
        private DBBusiness db = new();
        private MediaItem currentTour;
        private MediaFolder folder;
        private string fromDest;
        private string toDest;
        private DataTable logDataTable;

        private const decimal Unity = 1;
        private decimal _scale = Unity;
        public decimal ScaleStep => 0.1m;
        public decimal MinimumScale => 0.1m;
        public decimal MaximumScale => 4.0m;
        //Code for zoom commands snacked from: https://www.carlosjanderson.com/let-users-zoom-in-or-out-of-a-wpf-view/

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
    }
}
