using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class FollowTour_ViewModel : ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourInstanceService _tourInstanceService;
        private readonly LocationService _locationService;
        //private readonly
        private Tour _tour;
        public Tour Tour
        {
            get => _tour;
            set
            {
                _tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }
        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        private TourInstance _tourInstance;
        public TourInstance TourInstance
        {
            get => _tourInstance;
            set
            {
                _tourInstance = value;
                OnPropertyChanged(nameof(TourInstance));
            }
        }
        private List<BitmapImage> bitmapImages;
        public List<BitmapImage> BitmapImages
        {
            get => bitmapImages;
            set
            {
                bitmapImages = value;
                OnPropertyChanged(nameof(BitmapImages));
            }
        }
        private Visibility _isStarted;
        public Visibility IsStarted
        {
            get { return _isStarted; }
            set
            {
                if (_isStarted != value)
                {
                    _isStarted = value;
                    OnPropertyChanged(nameof(IsStarted));
                }
            }
        }
        private Visibility _isEnded;
        public Visibility IsEnded
        {
            get { return _isEnded; }
            set
            {
                if (_isEnded != value)
                {
                    _isEnded = value;
                    OnPropertyChanged(nameof(IsEnded));
                }
            }
        }
        private Visibility _infoText;
        public Visibility infoText
        {
            get { return _infoText; }
            set
            {
                if (_infoText != value)
                {
                    _infoText = value;
                    OnPropertyChanged(nameof(infoText));
                }
            }
        }
        public ICommand StartTourCommand { get; }
        public ICommand EndTourCommand { get; }
        public FollowTour_ViewModel() 
        {
            _tourService = new TourService();
            _tourInstanceService = new TourInstanceService();
            _locationService = new LocationService();
            Tour = _tourService.GetByActivity();
            StartTourCommand = new ViewModelCommandd(StartTour);
            EndTourCommand = new ViewModelCommandd(EndTour);
            TourInstance = _tourInstanceService.GetByActivity();
            IsEnded = Visibility.Hidden;
            IsStarted = Visibility.Hidden;
            infoText = Visibility.Hidden;
            Location = LoadLocation(Tour.LocationId);
            LoadImages(Tour.Images);
        }
        public void LoadImages(List<string> Images)
        {
            BitmapImages = new List<BitmapImage>();

            foreach (string imageName in Images)
            {
                string baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");
                // string imagePath = Path.Combine("D:\\Mila\\AHHHHHHHHHHHH\\sims-in-2024-group-2-team-c\\Resources\\Images\\", imageName);
                // string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", imageName);
                //string imagePath = Path.Combine("\\Resources\\Images\\", imageName);

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Find the index of the substring "\bin\Debug\net6.0-windows\"
                int index = baseDirectory.IndexOf("\\bin\\Debug\\net6.0-windows\\", StringComparison.OrdinalIgnoreCase);

                // Remove the substring "\bin\Debug\net6.0-windows\" from the base directory
                string parentDirectory = baseDirectory.Remove(index);

                // Construct the new path
                string newPath = Path.Combine(parentDirectory, "Resources", "Images", imageName);

                // Use the new path
                string imagePath = newPath;
                if (File.Exists(imagePath))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                    BitmapImages.Add(bitmap);
                }
                else
                {
                    // Handle missing image file
                }
            }
        }
        public string LoadLocation(int locationId)
        {
            Location location = _locationService.GetById(locationId);
            string ViewLocation = $"{location.City}, {location.Country}";
            return ViewLocation;
        }

        private void StartTour(object obj)
        {
            
            if (TourInstance != null)
            {
                TourInstance.Started = true;
                //MarkFirstPointActive(SelectedTour.KeyPointIds);
                _tourInstanceService.Update(TourInstance);
                //InfoTextBlock.Text = "Tour started successfully.";
                IsEnded = Visibility.Hidden;
                IsStarted = Visibility.Visible;
                infoText = Visibility.Hidden;
            }
            else
            {
                //.Text = "Tour instance is not available.";
                IsEnded = Visibility.Hidden;
                IsStarted = Visibility.Hidden;
                infoText = Visibility.Visible;
            }
        }

        private void EndTour(object obj)
        {
            if (TourInstance != null)
            {
                TourInstance.Ended = true;
                _tourInstanceService.Update(TourInstance);
                IsEnded = Visibility.Visible;
                IsStarted = Visibility.Hidden;
                infoText = Visibility.Hidden;
            }
            else
            {
                //InfoTextBlock.Text = "Tour instance is not available.";
                //InfoTextBlock.Visibility = Visibility.Visible;
                IsEnded = Visibility.Hidden;
                IsStarted = Visibility.Hidden;
                infoText = Visibility.Visible;
            }
        }
    }
}
