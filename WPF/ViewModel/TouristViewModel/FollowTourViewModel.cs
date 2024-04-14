using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class FollowTourViewModel : ViewModelBase
    {
        


        private ObservableCollection<FollowingTourDTO> _followingTours;

        public ObservableCollection<FollowingTourDTO> FollowingTours
        {
            get { return _followingTours; }
            set
            {
                _followingTours = value;
                OnPropertyChanged(nameof(FollowingTours));
            }
        }

        private readonly FollowTourService _followTourService;

        public ICommand ViewCommand { get;}

        
        private readonly MainViewModel _mainViewModel;
        public FollowTourViewModel() {

            _followTourService = new(new TourRepository(),new TourInstanceRepository());
            _mainViewModel = LoggedInUser.mainViewModel;
            FollowingTours = new ObservableCollection<FollowingTourDTO>();
            ViewCommand = new ViewModelCommand(ShowKeyPoints);
            LoadAciveTours();
        }


        private void LoadAciveTours()
        {
            try
            {
                
                var activeFollowingTours = _followTourService.GetActiveTourInstances();
                // Update the ObservableCollection with the active tours
               FollowingTours.Clear();
                foreach (var tour in activeFollowingTours)
                {
                    FollowingTours.Add(tour);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., log error, show message to the user, etc.)
                Console.WriteLine($"Error loading tours: {ex.Message}");
            }
        }


        private void ShowKeyPoints(object obj)
        {
           
                
           
        }




    }
}
