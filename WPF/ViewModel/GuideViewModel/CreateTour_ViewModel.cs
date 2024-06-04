using BookingApp.Commands;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class CreateTour_ViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly KeyPointService _keyPointService;
        //private readonly NavigationService _navigationService;
        private readonly MainWindow_ViewModel mainView;
        //private readonly TourGuide_MainWindow mainView = new TourGuide_MainWindow();
        private string _name;
        private string _location;
        private string _description;
        private string _language;
        private int _maxTourists;
        //private string _startingPoint;
        private string _startingPoint;
        private string _endingPoint;
        private DateTime _selectedDate;
        private List<DateTime> _dates = new List<DateTime>();
        //private string _dates;
        private int _duration;
        private string _images;
        private List<KeyPoint> _keyPoints = new List<KeyPoint>();

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
        private Visibility _isFilled = Visibility.Hidden;
        public Visibility IsFilled
        {
            get { return _isFilled; }
            set { _isFilled = value; OnPropertyChanged(nameof(IsFilled)); }
        }

        public ObservableCollection<int> KeyPointIds { get; set; }
        public ObservableCollection<DateTime> TourDates { get; set; }

        public ICommand SaveTourCommand { get; }
        public ICommand AddDateCommand { get; }
        //public ICommand AddKeyPointsCommand { get; }


        public CreateTour_ViewModel()
        {
            _tourService = new TourService();
            _keyPointService = new KeyPointService();
            //Frame mainFrame = mainView.MainFrame;
            mainView = LoggedInUser.mainGuideViewModel;
           // _navigationService = new NavigationService(mainFrame);
            KeyPointIds = new ObservableCollection<int>();
            TourDates = new ObservableCollection<DateTime>();
            IsSaved = Visibility.Hidden;
            IsFilled = Visibility.Hidden;
            SaveTourCommand = new RelayCommand(SaveTour);
            //BackCommand = new RelayCommand(Back);
            //ViewAllToursCommand = new RelayCommand(ViewAllTours);
            AddDateCommand = new RelayCommand(AddDate);
            //AddKeyPointsCommand = new RelayCommand(AddKeyPoints);
        }

        private void SaveTour()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Location) || string.IsNullOrEmpty(Description) ||
               string.IsNullOrEmpty(Language) || MaxTourists <= 0 /* || Dates.Count == 0*/ || Duration <= 0 ||
               string.IsNullOrEmpty(Images))
            {
                IsFilled = Visibility.Visible;
                //return;
            }
            KeyPoints = GetKeyPoints();
            //var keyPoints = new List<string> { StartingPoint, EndingPoint };
            var keyPoints = ParseKeyPointIds(KeyPoints);
            var imagePathsList = new List<string> { Images };
            string[] location = Location.Split(',');

            _tourService.CreateTour(Name, location[0], location[1], Description, Language, MaxTourists, keyPoints, Dates, Duration, imagePathsList);
            IsSaved = Visibility.Visible;
            IsFilled = Visibility.Hidden;
        }

        /*private void Back()
        {
            // Navigate to the previous page
        }

        private void ViewAllTours()
        {
            // Navigate to the View All Tours page
        }*/

        public List<KeyPoint> GetKeyPoints()
        {
            List<KeyPoint> keyPoints = new List<KeyPoint>();
            KeyPoint startPoint = _keyPointService.SaveKeyPoint(StartingPoint, true, false);
            KeyPoint endPoint = _keyPointService.SaveKeyPoint(EndingPoint, false, true);
            keyPoints.Add(startPoint); 
            keyPoints.Add(endPoint);

            return keyPoints;
            //Frame mainFrame = mainView.MainFrame;
            //mainFrame.Navigate(nextWindow);
            //this._navigationService.NavigateToPage(nextWindow);

            /*if (!string.IsNullOrEmpty(StartingPoint) && !string.IsNullOrEmpty(EndingPoint))
            {
                // Example logic, you might need to implement logic to get keyPoint Ids.
                KeyPointIds.Add(1); // Example Id for Start Point
                KeyPointIds.Add(2); // Example Id for End Point
            }*/
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
    }
}
