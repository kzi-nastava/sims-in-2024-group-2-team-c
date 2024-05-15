using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.Service.AccommodationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class RatingsByTheOwnerViewModel : ViewModelBase
    {
        private readonly GuestRatingService _guestRatingService;
        private GuestReservationDTO _selectedReservation;
        private int _cleanlinessRating;
        private int _ruleRespecting;
        private string _comment;

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

        public int RuleRespecting
        {
            get { return _ruleRespecting; }
            set
            {
                if (_ruleRespecting != value)
                {
                    _ruleRespecting = value;
                    OnPropertyChanged(nameof(RuleRespecting));
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

        public GuestReservationDTO SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        private string _ownerUsername;

        public string OwnerUsername
        {
            get { return _ownerUsername; }
            set
            {
                _ownerUsername = value;
                OnPropertyChanged(nameof(OwnerUsername));
            }
        }

        public RatingsByTheOwnerViewModel(GuestReservationDTO selectedReservation)
        {
            SelectedReservation = selectedReservation;
            OwnerUsername = selectedReservation.OwnerUsername;
            _guestRatingService = new GuestRatingService();

            LoadRatings();
        }

        private void LoadRatings()
        {
            GuestRating rating = _guestRatingService.GetRatingsByOwnerUsername(OwnerUsername);

            //if (ratings.Count > 0)
            //{
            Cleanliness = (int)rating.Cleanliness; //s.Average(r => r.Cleanliness);
            RuleRespecting = (int)rating.RuleRespecting; //s.Average(r => r.RuleRespecting);
            Comment = rating.Comment; //string.Join("\n", ratings.Select(r => r.Comment));
            //}
        }
    }
}
