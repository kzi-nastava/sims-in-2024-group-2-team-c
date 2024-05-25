using BookingApp.Commands;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        private int _currentImageIndex;

        private BitmapImage _currentImage;

        public BitmapImage CurrentImage
           {
            get { return _currentImage; }
            set{
                    _currentImage = value;
                    OnPropertyChanged(nameof(CurrentImage));
               
                }
            }
        public ViewModelCommandd NextImageCommand { get;  }

        public SelectedTourViewModel(HomeTourDTO selectedTour) { 
            SelectedTour = selectedTour;
            _followTourService = new FollowTourService();
            _tourInstanceService = new TourInstanceService();
            LoadKeyPointsForTour(SelectedTour);
            LoadTourInstances(SelectedTour.TourId);
            TourDescription = _followTourService.GetDescription(SelectedTour.TourId);

            
            _currentImageIndex = 1;
            UpdateCurrentImage();
            NextImageCommand = new ViewModelCommandd(NextImage);

        }


        private void NextImage(object obj)
        {
            _currentImageIndex = (_currentImageIndex + 1) % SelectedTour.Images.Count;
            UpdateCurrentImage();
        }

        private void UpdateCurrentImage()
        {
            CurrentImage = SelectedTour.Images[_currentImageIndex];
            
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
