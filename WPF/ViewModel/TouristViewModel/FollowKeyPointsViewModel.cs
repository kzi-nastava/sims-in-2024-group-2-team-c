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

        private readonly FollowTourService _followingService;

        public FollowKeyPointsViewModel(FollowingTourDTO selectedTour)
        {
            SelectedTour = selectedTour;
            _followingService = new(new TourRepository(), new TourInstanceRepository());
            _tourInstanceService = new(new TourInstanceRepository());
            IsTourInstanceEnded = false;

            // Use the selected tour data as needed
            LoadKeyPointsForTour(SelectedTour);
            CheckTourInstanceEnded(selectedTour.TourInstanceId);
        }

        private void LoadKeyPointsForTour(FollowingTourDTO SelectedTour)
        {
            // Your implementation for loading key points for the selected tour

            ActiveTourKeyPoints = new ObservableCollection<ActiveTourKeyPointDTO>(_followingService.GetKeyPoints(SelectedTour));

            

        }


        private void CheckTourInstanceEnded(int tourInstanceId)
        {
            // Fetch tour instance data from the data source
            var tourInstance = _tourInstanceService.GetById(tourInstanceId);

            // Update IsTourInstanceEnded based on tour instance data
            if (tourInstance != null)
            {
                IsTourInstanceEnded = tourInstance.Ended;
            }
        }


    }
}
