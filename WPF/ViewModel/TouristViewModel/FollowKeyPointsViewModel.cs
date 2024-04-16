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

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class FollowKeyPointsViewModel : ViewModelBase
    {
        
        private readonly MainViewModel _mainViewModel;
        private FollowingTourDTO _selectedTour;
        private readonly TourInstanceService _tourInstanceService;
        private readonly TouristService _touristService;
        private readonly FollowTourService _followingService;

        public FollowingTourDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        private bool _isTourInstanceEnded;
        public bool IsTourInstanceEnded
        {
            get { return _isTourInstanceEnded; }
            set
            {
                _isTourInstanceEnded = value;
                OnPropertyChanged(nameof(IsTourInstanceEnded));
            }
        }



        private ObservableCollection<ActiveTourKeyPointDTO> _activeTourKeyPoints;

        public ObservableCollection<ActiveTourKeyPointDTO> ActiveTourKeyPoints
        {
            get { return _activeTourKeyPoints; }
            set
            {
                _activeTourKeyPoints = value;
                OnPropertyChanged(nameof(ActiveTourKeyPoints));
            }
        }

        

        public FollowKeyPointsViewModel(FollowingTourDTO selectedTour)
        {
            SelectedTour = selectedTour;
            _followingService = new FollowTourService();
            _tourInstanceService = new(new TourInstanceRepository());
            IsTourInstanceEnded = false;
            _touristService = new TouristService(new TouristRepository());
            _touristService.Activate(LoggedInUser.Id);


            
            LoadKeyPointsForTour(SelectedTour);
            CheckTourInstanceEnded(selectedTour.TourInstanceId);
        }

        private void LoadKeyPointsForTour(FollowingTourDTO SelectedTour)
        {
            
            ActiveTourKeyPoints = new ObservableCollection<ActiveTourKeyPointDTO>(_followingService.GetKeyPoints(SelectedTour));
 
        }


        private void CheckTourInstanceEnded(int tourInstanceId)
        {
           
            var tourInstance = _tourInstanceService.GetById(tourInstanceId);

            // Update IsTourInstanceEnded based on tour instance data
            if (tourInstance != null)
            {
                IsTourInstanceEnded = tourInstance.Ended;
            }
        }


    }
}
