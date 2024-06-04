using BookingApp.Commands;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class CreateTourByRequest_ViewModel : ViewModelBase
    {
        private readonly TourService tourService;
        private readonly KeyPointService keyPointService;
        private string _name;
        private string _location;
        private string _description;
        private string _language;
        private int _maxTourists;
        private List<KeyPoint> _keyPoints;
        private string _startingPoint;
        private string _endingPoint;
        private DateTime _selectedDate;
        private List<DateTime> _dates = new List<DateTime>();
        //private string _dates;
        private int _duration;
        private string _images;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged(nameof(Location)); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; OnPropertyChanged(nameof(Language)); }
        }

        public int MaxTourists
        {
            get { return _maxTourists; }
            set { _maxTourists = value; OnPropertyChanged(nameof(MaxTourists)); }
        }
        public List<KeyPoint> KeyPoints
        {
            get { return _keyPoints; }
            set { _keyPoints = value; OnPropertyChanged(nameof(KeyPoints)); }
        }
        public string StartingPoint
        {
            get { return _startingPoint; }
            set { _startingPoint = value; OnPropertyChanged(nameof(StartingPoint)); }
        }

        public string EndingPoint
        {
            get { return _endingPoint; }
            set { _endingPoint = value; OnPropertyChanged(nameof(EndingPoint)); }
        }

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }

        public List<DateTime> Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged(nameof(Dates)); }
        }
        /*public string Dates
        {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged(nameof(Dates)); }
        }*/
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public string Images
        {
            get { return _images; }
            set { _images = value; OnPropertyChanged(nameof(Images)); }
        }
        private Visibility _isSaved;
        public Visibility IsSaved
        {
            get { return _isSaved; }
            set { _isSaved = value; OnPropertyChanged(nameof(IsSaved)); }
        }
        private Visibility _isFilled;
        public Visibility IsFilled
        {
            get { return _isFilled; }
            set { _isFilled = value; OnPropertyChanged(nameof(IsFilled)); }
        }

        public ICommand SaveTourCommand { get; }
        public ICommand AddDateCommand { get; }
        public ObservableCollection<int> KeyPointIds { get; set; }
        public ObservableCollection<DateTime> TourDates { get; set; }

        public CreateTourByRequest_ViewModel()
        {
            tourService = new TourService();
            keyPointService = new KeyPointService(); 
            SaveTourCommand = new ViewModelCommandd(SaveTour);
            AddDateCommand = new RelayCommand(AddDate);
            // SaveTourCommand = new Commands.RelayCommand(SaveTour);
            KeyPointIds = new ObservableCollection<int>();
            TourDates = new ObservableCollection<DateTime>();
            IsSaved = Visibility.Hidden;
            IsFilled = Visibility.Hidden;
        }
        public void AddDateToList(DateTime date)
        {
            Dates.Add(date);
        }

        private void AddDate()
        {
            if (SelectedDate != default)
            {
                Dates.Add(SelectedDate);
                SelectedDate = default;
            }
        }
        private void SaveTour(object obj)
        {
            if(AreAllFieldsFilled())
            {
                CreateTourByRequests();
                IsSaved = Visibility.Visible;
                IsFilled = Visibility.Hidden;
            }
            else
            {
                IsSaved = Visibility.Hidden;
                IsFilled = Visibility.Visible;
            }
            
        }
       /* private void SaveTour()
        {
            if(AreAllFieldsFilled())
            {
                CreateTourByRequests();
                IsSaved = Visibility.Visible;
                IsFilled = Visibility.Hidden;
            }
            else
            {
                IsSaved = Visibility.Hidden;
                IsFilled = Visibility.Visible;
            }
            
        }*/

        public void CreateTourByRequests()
        {
            //List<string> keyPointList = KeyPoints.Split(',').ToList();
            //List<int> keyPointsIds = keyPointService.ParseKeyPointIds(keyPointList);
            KeyPoints = GetKeyPoints();
            var keyPoints = ParseKeyPointIds(KeyPoints);
            List<string> imageList = Images.Split(',').ToList();
            string[] locationList = Location.Split(",");
            //List<DateTime> tourDates = ParseTourDates(Dates);
            tourService.CreateTour(Name, locationList[0].Trim(), locationList[1].Trim(), Description, Language, MaxTourists, keyPoints, Dates, Duration, imageList);
        }
        public List<KeyPoint> GetKeyPoints()
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            KeyPoint startPoint = keyPointService.SaveKeyPoint(StartingPoint, true, false);
            KeyPoint endPoint = keyPointService.SaveKeyPoint(EndingPoint, false, true);
            keyPoints.Add(startPoint);
            keyPoints.Add(endPoint);

            return keyPoints;
        }
        private List<int> ParseKeyPointIds(List<KeyPoint> keyPointsList)
        {
            List<int> ids = new List<int>();

            foreach (KeyPoint kp in keyPointsList)
            {
                ids.Add(kp.Id);
            }
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
        private bool AreAllFieldsFilled()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(Description) /*||
                string.IsNullOrWhiteSpace(LanguageTextBox.Text) ||
                string.IsNullOrWhiteSpace(MaxTourists) ||
                string.IsNullOrWhiteSpace(KeyPoints) ||
                string.IsNullOrWhiteSpace(Dates) ||
                string.IsNullOrWhiteSpace(Duration)||
                string.IsNullOrWhiteSpace(ImagesTextBox.Text)*/)
            {
                return false;
            }
            return true;
        }
    }
}
