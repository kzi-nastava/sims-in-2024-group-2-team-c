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

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for Guide_TutorialView.xaml
    /// </summary>
    public partial class Guide_TutorialView : Page
    {
        public Guide_TutorialView()
        {
            InitializeComponent();
            TutorialVideo.Source = new Uri("path/to/your/video.mp4", UriKind.Relative); // Set the path to your video file
            TutorialVideo.LoadedBehavior = MediaState.Manual;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.GoBack();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            TutorialVideo.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            TutorialVideo.Pause();
        }
    }
}
