using TourPlanner.Model;

namespace TourPlanner.DesignViewModel
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel()
        {
            this.CurrentUsername = "TestUsername";
            this.CurrentPoints = "TestPoints";

            this.Data.Clear();
            this.Data.Add(new HighscoreEntry("First", "a million"));
            this.Data.Add(new HighscoreEntry("Second", "a billion"));
            this.Data.Add(new HighscoreEntry("Third", "a trillion"));
            this.Data.Add(new HighscoreEntry("Fourth", "more..."));
        }
    }
}
