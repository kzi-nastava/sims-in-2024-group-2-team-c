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
        private readonly FollowTourService _followTourService;
        private readonly MainViewModel _mainViewModel;

        public ObservableCollection<FollowingTourDTO> FollowingTours
        {
            get { return _followingTours; }
            set
            {
                _followingTours = value;
                OnPropertyChanged(nameof(FollowingTours));
            }
        }

        
        public ICommand ViewCommand { get;}


        
        public FollowTourViewModel() {

            _followTourService = new FollowTourService();
            _mainViewModel = LoggedInUser.mainViewModel;
            FollowingTours = new ObservableCollection<FollowingTourDTO>();
           // ViewCommand = new ViewModelCommand(ShowKeyPoints);
            LoadAciveTours();
        }


        private void LoadAciveTours()
        {
            try
            {
                GetTours();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error loading tours: {ex.Message}");
            }
        }

        private void GetTours()
        {
            var activeFollowingTours = _followTourService.GetActiveTourInstances();
            
            FollowingTours.Clear();
            foreach (var tour in activeFollowingTours)
            {
                FollowingTours.Add(tour);
            }
        }

        private void ShowKeyPoints(object obj)
        {
           

                
           
        }




    }
}
