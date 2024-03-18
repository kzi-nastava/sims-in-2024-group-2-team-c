using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly KeyPointRepository _keyPointRepository;
        private readonly TourInstanceRepository _tourInstanceRepository;

        public TourForm()
        {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _keyPointRepository = new KeyPointRepository();
            _tourInstanceRepository = new TourInstanceRepository();
        }

        private void SaveTour_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {*/
                string name = NameTextBox.Text;
                string[] locationData = LocationTextBox.Text.Split(',');
                /*if (locationData.Length != 2)
                {
                    MessageBox.Show("Unesite lokaciju u formatu 'grad,drzava'.");
                    return;
                }*/
                string city = locationData[0].Trim();
                string country = locationData[1].Trim();
                string description = DescriptionTextBox.Text;
                string language = LanguageTextBox.Text;
                //int maxTourists = int.Parse(MaxTouristsTextBox.Text);
                int maxTourists = Convert.ToInt32(MaxTouristsTextBox.Text);
                List<string> keyPointsList = KeyPointsTextBox.Text.Split(',').Select(s => s.Trim()).ToList();
                List<int> keyPointIds = ParseKeyPointIds(keyPointsList);
                List<DateTime> tourDates = ParseTourDates(DatesTextBox.Text);
                //int duration = int.Parse(DurationTextBox.Text);
                int duration = Convert.ToInt32(DurationTextBox.Text);
                List<string> imagePaths = ImagesTextBox.Text.Split(',').Select(s => s.Trim()).ToList();

                // Kreiranje nove ture
                CreateTour(name, city, country, description, language, maxTourists, keyPointIds, tourDates, duration, imagePaths);
                infoTextBlock.Visibility = Visibility.Visible;
            //MessageBox.Show("Nova tura je uspešno kreirana.");
            /*}
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri kreiranju ture: {ex.Message}");
            }*/
        }

        private List<int> ParseKeyPointIds(List<string> keyPointsList)
        {
            List<int> ids = new List<int>();
            KeyPoint startedPoint = new KeyPoint();
            startedPoint.Name = keyPointsList[0];
            startedPoint.StartingPoint = true;
            ids.Add(startedPoint.Id);
            _keyPointRepository.Save(startedPoint);

            for (int i = 1; i < keyPointsList.Count - 1; i++)
            {
                KeyPoint kp = new KeyPoint();
                kp.Name = keyPointsList[i];
                kp.StartingPoint = false;
                kp.EndingPoint = false;
                ids.Add(kp.Id);
                _keyPointRepository.Save(kp);
            }
            KeyPoint endedPoint = new KeyPoint();
            endedPoint.Name = keyPointsList[keyPointsList.Count - 1];
            endedPoint.EndingPoint = true;
            ids.Add(endedPoint.Id);
            _keyPointRepository.Save(endedPoint);

            return ids;
        }

        private List<DateTime> ParseTourDates(string tourDatesString)
        {
            //List<DateTime> tourDates = tourDatesString.Split(',').Select(s => DateTime.Parse(s.Trim())).ToList();
            List<string> list = tourDatesString.Split(',').ToList();
            List<DateTime> tourDates = new List<DateTime>();
            foreach (var dt in list)
            {
                DateTime converted = Convert.ToDateTime(dt);
                tourDates.Add(converted);
            }
            return tourDates;
        }


        private void CreateTour(string name, string city, string country, string description, string language, int maxTourists, List<int> keyPointIds, List<DateTime> tourDates, int duration, List<string> imagePaths)
        {
            
            Location location = _locationRepository.FindLocation(city, country);
            if (location == null)
            {
                location = new Location(city, country);
                _locationRepository.Save(location);
            }
            
            if (keyPointIds.Count < 2)
            {
                throw new ArgumentException("Tura mora da sadrži barem dve ključne tačke.");
            }
            
            Tour newTour = new Tour
            {
                Name = name,
                LocationId = location.Id,
                Description = description,
                Language = language,
                KeyPointIds = keyPointIds,
                Duration = duration,
                Images = imagePaths,
            };
            _tourRepository.Save(newTour);
            //kreiranje instanci ture
            List<int> tourInstancesids = new List<int>();

            for (int i = 0; i < tourDates.Count; i++)
            {
                TourInstance tourInstance = new TourInstance(newTour.Id, maxTourists, 0, false, false, tourDates[i]);
                _tourInstanceRepository.Save(tourInstance);
                int tourInstanceid = tourInstance.Id;
                tourInstancesids.Add(tourInstanceid);
            }

        }
    }
}

