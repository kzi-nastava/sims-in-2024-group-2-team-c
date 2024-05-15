using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.Service.AccommodationServices;
using BookingApp.WPF.View.GuestView;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using NavigationService = System.Windows.Navigation.NavigationService;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class AccommodationRateViewModel : ViewModelBase
    {

        private readonly AccommodationRateService _service;
        private int _cleanlinessRating;
        private int _correctnessOfTheOwnerRating;
        private string _comment;
        private NavigationService _navigationService;

        public ICommand RateAccommodationCommand { get; }
        public ICommand RenovateAccommodationCommand { get; }
        public ICommand AddPictureCommand { get; }

        private bool _isReservationRated;
        public bool IsReservationRated
        {
            get { return _isReservationRated; }
            set
            {
                _isReservationRated = value;
                OnPropertyChanged(nameof(IsReservationRated));
            }
        }

        private bool _isRated;
        public bool IsRated
        {
            get { return _isRated; }
            set
            {
                _isRated = value;
                OnPropertyChanged(nameof(IsRated));
            }
        }

        private bool CanRenovate
        {
            get { return IsRated; }
        }

        public int Cleanliness
        {
            get { return _cleanlinessRating; }
            set
            {
                if (value >= 1 && value <= 5)
                {
                    if (_cleanlinessRating != value)
                    {
                        _cleanlinessRating = value;
                        OnPropertyChanged(nameof(Cleanliness));
                    }
                }
                else
                {
                    MessageBox.Show("Values can only be between 1 and 5.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public int CorrectnessOfTheOwner
        {
            get { return _correctnessOfTheOwnerRating; }
            set
            {
                if (value >= 1 && value <= 5)
                {
                    if (_correctnessOfTheOwnerRating != value)
                    {
                        _correctnessOfTheOwnerRating = value;
                        OnPropertyChanged(nameof(CorrectnessOfTheOwner));
                    }
                }
                else
                {
                    MessageBox.Show("Values can only be between 1 and 5.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private List<string> _images = new List<string>();
        public List<string> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
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
        private AccommodationRate accommodationRate;
        public AccommodationRate AccommodationRate
        {
            get { return accommodationRate; }
            set
            {
                if (accommodationRate != value)
                {
                    accommodationRate = value;
                    OnPropertyChanged(nameof(AccommodationRate));
                }
            }
        }

        private void NavigateToRenovatePage(AccommodationRate accommodationRate)
        {
            if (!CanRenovate)
            {
                MessageBox.Show("Please rate the accommodation first before renovating.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (AccommodationRate == null)
            {
                MessageBox.Show("Accommodation rate is not yet available.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _navigationService?.Navigate(new AccommodationRenovateView(AccommodationRate));
        }

        private void RateAccommodation(GuestReservationDTO guestReservationDTO)
        {
            if (IsReservationRated)
            {
                MessageBox.Show("You have already rated this reservation.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Cleanliness == 0 || CorrectnessOfTheOwner == 0)
            {
                MessageBox.Show("Please fill in Cleanliness and Correctness of the owner fields to rate the accommodation.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            AccommodationRate = _service.RateAccommodation(Cleanliness, CorrectnessOfTheOwner, Comment, Images ?? new List<string>());

            IsRated = true;
            IsReservationRated = true;
        }

        public AccommodationRateViewModel(GuestReservationDTO selectedReservation, NavigationService navigationService)
        {
            Cleanliness = 1;
            CorrectnessOfTheOwner = 1;
            IsRated = false;
            IsReservationRated = false;
            Name = selectedReservation.Name;
            _service = new AccommodationRateService(selectedReservation);
            RateAccommodationCommand = new ViewModelCommand<GuestReservationDTO>(RateAccommodation);
            RenovateAccommodationCommand = new ViewModelCommand<AccommodationRate>((rate) => NavigateToRenovatePage(rate));
            _navigationService = navigationService;
            AddPictureCommand = new ViewModelCommandd(OpenFileExplorer);
        }

        private void OpenFileExplorer(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*" //"Image Files|.jpg;.jpeg;.png;.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string destinationFolder = "C:/Users/Sara/Documents/SIMS/Projekat_Novi_5/sims-in-2024-group-2-team-c/Resources/Images/";
                Directory.CreateDirectory(destinationFolder);



                foreach (string filePath in openFileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(filePath);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    File.Copy(filePath, destinationPath, true);
                    Images.Add(fileName);
                }
            }
        }

        private void ShowDialog(OpenFileDialog openFileDialog)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                Images = new List<string>(openFileDialog.FileNames);
                string selectedFiles = string.Join("\n", Images);

            }
        }

    }
}
