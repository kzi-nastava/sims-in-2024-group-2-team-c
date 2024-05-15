using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class RegisterAccommodationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly AccommodationRepository _repository;
        private readonly LocationRepository locationRepository;
        List<string> imagePaths = new List<string>();
        public List<string> Images;

        private LoggedInUser _loggedInUser;
        public LoggedInUser LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                _maxGuests = value;
                OnPropertyChanged(nameof(MaxGuests));
            }
        }

        private int _minBookingDays;
        public int MinBookingDays
        {
            get { return _minBookingDays; }
            set
            {
                _minBookingDays = value;
                OnPropertyChanged(nameof(MinBookingDays));
            }
        }

        private int _cancellationDays;
        public int CancellationDays
        {
            get { return _cancellationDays; }
            set
            {
                _cancellationDays = value;
                OnPropertyChanged(nameof(CancellationDays));
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }


        public RegisterAccommodationViewModel()
        {
            
            _repository = new AccommodationRepository();
            locationRepository = new LocationRepository();

        }

        public void SaveAccommodation(object sender, RoutedEventArgs e)
        {

            if (!ValidateInput())
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            string name = Name.Trim();
            string city = City.Trim().ToLower();
            Location location = locationRepository.GetLocationByCity(city);
            string type = Type.Trim();
            int maxGuests = MaxGuests;
            int minBookingDays = MinBookingDays;
            int cancellationDays = CancellationDays;

            string[] paths = ImagePath.Split('|');


            foreach (string path in paths)
            {

                if (!string.IsNullOrEmpty(path))
                {
                    imagePaths.Add(path);
                }
            }

            Accommodation newAccommodation = new Accommodation()
            {
                Name = name,
                Location = location,
                Type = type,
                MaxGuests = maxGuests,
                MinBookingDays = minBookingDays,
                CancellationDays = cancellationDays,
                Owner = new Owner { Id = LoggedInUser.Id },
                Images = imagePaths
            };

            _repository.Save(newAccommodation);


            MessageBox.Show("Accommodation registered successfully.");

            // Close();
        

    }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(City) ||
                string.IsNullOrEmpty(Type) ||
                MaxGuests <= 0 ||
                MinBookingDays <= 0 ||
                CancellationDays <= 0) //||
               // string.IsNullOrEmpty(ImagePath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {

                string[] selectedFiles = openFileDialog.FileNames;


                foreach (string fileName in selectedFiles)
                {
                    ImagePath += fileName + Environment.NewLine;
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
