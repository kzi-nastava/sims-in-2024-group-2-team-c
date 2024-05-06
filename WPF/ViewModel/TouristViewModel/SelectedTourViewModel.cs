using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SelectedTourViewModel : ViewModelBase
    {

        private HomeTourDTO _selectedTour;


        public HomeTourDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }


        private ObservableCollection<ActiveTourKeyPointDTO> _tourKeyPoints;

        public ObservableCollection<ActiveTourKeyPointDTO> TourKeyPoints
        {
            get { return _tourKeyPoints; }
            set
            {
                _tourKeyPoints = value;
                OnPropertyChanged(nameof(TourKeyPoints));
            }
        }


        private ObservableCollection<TourInstance> _tourInstances;

        public ObservableCollection<TourInstance> TourInstances
        {
            get { return _tourInstances; }
            set
            {
                _tourInstances = value;
                OnPropertyChanged(nameof(TourInstances));
            }
        }


        private string _tourDescription;
        public string TourDescription
        {
            get { return _tourDescription; }
            set
            {
                _tourDescription = value;
                OnPropertyChanged(nameof(TourDescription));
            }
        }

        private readonly FollowTourService _followTourService;
        private readonly TourInstanceService _tourInstanceService;
        

        public SelectedTourViewModel(HomeTourDTO selectedTour) { 
            SelectedTour = selectedTour;
            _followTourService = new FollowTourService();
            _tourInstanceService = new TourInstanceService();
            LoadKeyPointsForTour(SelectedTour);
            LoadTourInstances(SelectedTour.TourId);
            TourDescription = _followTourService.GetDescription(SelectedTour.TourId);
        }


        private void LoadKeyPointsForTour(HomeTourDTO SelectedTour)
        {

            TourKeyPoints = new ObservableCollection<ActiveTourKeyPointDTO>(_followTourService.GetKeyPointsByTour(SelectedTour));
            
        }

        private void LoadTourInstances(int id)
        {
            TourInstances = new ObservableCollection<TourInstance>(_tourInstanceService.GetTourInstancesByTourId(id));
           
        }


    }
}
