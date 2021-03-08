using TourPlanner.Model;

namespace TourPlanner.Model
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class HighscoreEntry : INotifyPropertyChanged
    {
        private string _username;
        private string _points;

        public HighscoreEntry(string username, string points)
        {
            this.Username = username;
            this.Points = points;
        }

        public string Username
        {
            get => this._username;
            set
            {
                this._username = value;
                this.OnPropertyChanged(); // using CallerMemberName
            }
        }

        public string Points
        {
            get => this._points;
            set
            {
                this._points = value;
                this.OnPropertyChanged(nameof(Points));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
