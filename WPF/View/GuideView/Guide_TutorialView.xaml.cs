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
        public string TutorialName { get; set; }
        public Guide_TutorialView(string tutorialName)
        {
            InitializeComponent();
            TutorialName = tutorialName;
            //TutorialVideo.Source = new Uri("path/to/your/video.mp4", UriKind.Relative); // putanja
            //string path = $"C:/Users/Korisnik/Desktop/anja/SIMS/sims-in-2024-group-2-team-c/Resources/Tutorials/" + TutorialName + ".mp4";
            //string path = AppDomain.CurrentDomain.BaseDirectory + $"Resources/Tutorials/" + TutorialName + $".mp4";
            //TutorialVideo.Source = new Uri(path, UriKind.Relative); // putanja
            this.TutorialVideo.LoadedBehavior = MediaState.Manual;
            //LoadVideo();
        }
        /*private void LoadVideo()
        {
            // Ensure the video file path is correct
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Tutorials", $"{TutorialName}.mp4");
            if (File.Exists(path))
            {
                TutorialVideo.Source = new Uri(path, UriKind.Absolute);
                TutorialVideo.LoadedBehavior = MediaState.Manual;
            }
            else
            {
                MessageBox.Show("The video file could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
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
