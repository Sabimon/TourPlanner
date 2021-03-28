using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private void ShowRouteButtonClick(object sender, RoutedEventArgs e)
        {
            this.imageView.Visibility = Visibility.Visible;
            this.listRoutes.Visibility = Visibility.Collapsed;
        }
        private void ShowDescriptionButtonClick(object sender, RoutedEventArgs e)
        {
            this.listRoutes.Visibility = Visibility.Visible;
            this.imageView.Visibility = Visibility.Collapsed;
        }
    }
}
