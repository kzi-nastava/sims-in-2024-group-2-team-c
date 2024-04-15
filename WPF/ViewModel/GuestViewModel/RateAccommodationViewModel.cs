using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class RateAccommodationViewModel : ViewModelBase
    {

        private readonly AccommodationRateService _service;
        private int _cleanlinessRating;
        private int _correctnessOfTheOwnerRating;
        private string _comment;

        public ICommand RateAccommodationCommand { get; }

        public int Cleanliness
        {
            get { return _cleanlinessRating; }
            set
            {
                if (_cleanlinessRating != value)
                {
                    _cleanlinessRating = value;
                    OnPropertyChanged(nameof(Cleanliness));
                }
            }
        }

        public int CorrectnessOfTheOwner
        {
            get { return _correctnessOfTheOwnerRating; }
            set
            {
                if (_correctnessOfTheOwnerRating != value)
                {
                    _correctnessOfTheOwnerRating = value;
                    OnPropertyChanged(nameof(CorrectnessOfTheOwner));
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

        /*public ICommand IncreaseCleanlinessCommand { get; }
        public ICommand DecreaseCleanlinessCommand { get; }
        public ICommand IncreaseCorrectnessOfTheOwnerCommand { get; }
        public ICommand DecreaseCorrectnessOfTheOwnerCommand { get; }

        private void IncreaseCleanliness(object obj)
        {
            // Povećaj ocenu čistoće, ali ne prelazi 5
            if (CleanlinessRating < 5)
            {
                CleanlinessRating++;
            }
        }

        private void DecreaseCleanliness(object obj)
        {
            // Smanji ocenu čistoće, ali ne ide ispod 1
            if (CleanlinessRating > 1)
            {
                CleanlinessRating--;
            }
        }

        private void IncreaseCorrectnessOfTheOwner(object obj)
        {
            // Povećaj ocenu čistoće, ali ne prelazi 5
            if (CorrectnessOfTheOwner < 5)
            {
                CorrectnessOfTheOwner++;
            }
        }

        private void DecreaseCorrectnessOfTheOwner(object obj)
        {
            // Smanji ocenu čistoće, ali ne ide ispod 1
            if (CorrectnessOfTheOwner > 1)
            {
                CorrectnessOfTheOwner--;
            }
        }
        */

        private void RateAccommodation(GuestReservationDTO selectedReservation)
        {
             _service.RateAccommodation(Cleanliness, CorrectnessOfTheOwner, Comment);
        }

        public RateAccommodationViewModel(GuestReservationDTO selectedReservation)
        {
            Cleanliness = 1;
            /*
            IncreaseCleanlinessCommand = new ViewModelCommand<object>(IncreaseCleanliness);
            DecreaseCleanlinessCommand = new ViewModelCommand<object>(DecreaseCleanliness);
            IncreaseCorrectnessOfTheOwnerCommand = new ViewModelCommand<object>(IncreaseCorrectnessOfTheOwner);
            DecreaseCorrectnessOfTheOwnerCommand = new ViewModelCommand<object>(DecreaseCorrectnessOfTheOwner);
            */
            _service = new AccommodationRateService(selectedReservation, new AccommodationRateRepository());
            RateAccommodationCommand = new ViewModelCommand<GuestReservationDTO>(RateAccommodation);
        }

    }
}
