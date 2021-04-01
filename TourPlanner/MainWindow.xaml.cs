using System.Windows;
using System.Windows.Media;
using TourPlannerDL;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZoomInButtonClick(object sender, RoutedEventArgs e)
        {
            var transform = (ScaleTransform)imageView.RenderTransform;
            transform.ScaleX *= 1.1;
            transform.ScaleY *= 1.1;
        }
        private void ZoomOutButtonClick(object sender, RoutedEventArgs e)
        {
            var transform = (ScaleTransform)imageView.RenderTransform;
            transform.ScaleX /= 1.1;
            transform.ScaleY /= 1.1;
        }
    }
}
