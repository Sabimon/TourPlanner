using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using TourPlanner.Model;

namespace TourPlanner
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<HighscoreEntry> Data { get; }
            = new ObservableCollection<HighscoreEntry>();

        public string CurrentUsername { get; set; }
        public string CurrentPoints { get; set; }
        public RelayCommand AddCommand { get; }

        private bool _isUsernameFocused;
        public bool IsUsernameFocused
        {
            get => _isUsernameFocused;
            set
            {
                // it needs to flip, else it does not execute properly, so let's reset here
                _isUsernameFocused = false;
                OnPropertyChanged();
                _isUsernameFocused = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            AddCommand = new RelayCommand((_) =>
            {
                Data.Add(new HighscoreEntry(this.CurrentUsername, this.CurrentPoints));
                CurrentUsername = string.Empty;
                CurrentPoints = string.Empty;
                OnPropertyChanged(nameof(CurrentUsername));
                OnPropertyChanged("CurrentPoints");
                IsUsernameFocused = true;
            });
            IsUsernameFocused = true;

            // real data to add (not design data)
            Data.Add(new HighscoreEntry("Daniel", "Infinite"));
            Data.Add(new HighscoreEntry("not Daniel", "few"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}