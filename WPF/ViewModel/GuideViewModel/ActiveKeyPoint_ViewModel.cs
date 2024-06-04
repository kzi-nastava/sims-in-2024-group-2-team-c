using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class ActiveKeyPoint_ViewModel : ViewModelBase
    {
        private readonly KeyPointRepository _keyPointRepository;
        private readonly KeyPointService _keyPointService;
        private readonly TouristRepository _touristRepository;
        private readonly PeopleInfoRepository _peopleInfoRepository;
        private readonly TourService _tourService;

        private ObservableCollection<KeyPoint> _keyPoints;
        public ObservableCollection<KeyPoint> KeyPoints
        {
            get { return _keyPoints; }
            set
            {
                if (_keyPoints != value)
                {
                    _keyPoints = value;
                    OnPropertyChanged(nameof(KeyPoints));
                }
            }
        }
        private List<KeyPoint> _toursKeyPoints= new List<KeyPoint>();
        //private List<KeyPoint> _toursKeyPoints;
        public List<KeyPoint> toursKeyPoints
        {
            get { return _toursKeyPoints; }
            set
            {
                if (_toursKeyPoints != value)
                {
                    _toursKeyPoints = value;
                    OnPropertyChanged(nameof(toursKeyPoints));
                }
            }
        }

        public ActiveKeyPoint_ViewModel() 
        {
            _keyPointRepository = new KeyPointRepository();
            _peopleInfoRepository = new PeopleInfoRepository();
            _touristRepository = new TouristRepository();
            _tourService = new TourService();
            _keyPointService = new KeyPointService();
            //KeyPoints = new ObservableCollection<KeyPoint>();
            LoadKeyPoints(toursKeyPoints);
        }
        private void LoadKeyPoints(List<KeyPoint> keyPoints)
        {
            Tour tour = _tourService.GetByActivity();
            keyPoints = _keyPointService.GetKeypointsByIds(tour.KeyPointIds);
            KeyPoints = new ObservableCollection<KeyPoint>(keyPoints);
            //keyPointsListView.ItemsSource = keyPoints;
        }
    }
}
