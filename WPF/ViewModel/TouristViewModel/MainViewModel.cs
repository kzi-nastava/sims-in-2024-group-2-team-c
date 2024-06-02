using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<PartViewModel> _currentParts;
        public ObservableCollection<PartViewModel> CurrentParts
        {
            get => _currentParts;
            set
            {
                _currentParts = value;
                OnPropertyChanged(nameof(CurrentParts));
            }
        }




        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }


        private string _currentHomeIconSource;

        public string CurrentHomeIconSource
        {
            get { return _currentHomeIconSource; }
            set
            {
                _currentHomeIconSource = value;
                OnPropertyChanged(nameof(CurrentHomeIconSource));
            }
        }

        private string _currentMarkerIconSource;

        public string CurrentMarkerIconSource
        {
            get { return _currentMarkerIconSource; }
            set
            {
                _currentMarkerIconSource = value;
                OnPropertyChanged(nameof(CurrentMarkerIconSource));
            }
        }


        private string _currentUserIconSource;

        public string CurrentUserIconSource
        {
            get { return _currentUserIconSource; }
            set
            {
                _currentUserIconSource = value;
                OnPropertyChanged(nameof(CurrentUserIconSource));
            }
        }


        private string _currentNotificationIconSource;

        public string CurrentNotificationIconSource
        {
            get { return _currentNotificationIconSource; }
            set
            {
                _currentNotificationIconSource = value;
                OnPropertyChanged(nameof(CurrentNotificationIconSource));
            }
        }


        private string _currentRequestIconSource;

        public string CurrentRequestIconSource
        {
            get { return _currentRequestIconSource; }
            set
            {
                _currentRequestIconSource = value;
                OnPropertyChanged(nameof(CurrentRequestIconSource));
            }
        }


        public ViewModelCommandd ShowToursCommand { get; }

        public ViewModelCommandd FollowTourCommand { get; }
        public ViewModelCommandd ShowKeyPointsCommand { get; }

        public ViewModelCommandd UserCommand { get; }

        public ViewModelCommandd NotificationCommand { get; }

        public ViewModelCommandd RequestsCommand { get; }

        private readonly TouristService touristService;
        private readonly TourVoucherService tourVoucherService;

        public MainViewModel()
        {

            LoggedInUser.mainViewModel = this;
            FollowTourCommand = new ViewModelCommandd(ExecuteFollowTourCommand);
            //ShowKeyPointsCommand = new ViewModelCommand(ExecuteShowKeyPointsCommand);
            UserCommand = new ViewModelCommandd(ExecuteUserCommand);
            NotificationCommand = new ViewModelCommandd(ExecuteNotificationCommand);
            ShowToursCommand = new ViewModelCommandd(ExecuteShowTourCommand);
            RequestsCommand = new ViewModelCommandd(ExecuteRequestCommand);

            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            tourVoucherService = new TourVoucherService();
            touristService = new TouristService();

            CheckForUniversalVoucher();

            

            CurrentChildView = new TouristHomeViewModel();
        }

        public void ExecuteNotificationCommand(object obj)
        {
            /* if (obj is MainTouristView mainTouristView)
             {
                 mainTouristView.MainFrame.Navigate(new FollowTourView());
             }*/
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/on bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new NotificationViewModel();

        }

        public void ExecuteFollowTourCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/on marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new FollowTourViewModel();



        }


        public void ExecuteShowTourCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentChildView = new TouristHomeViewModel();



        }

        public void ExecuteUserCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new TouristUserViewModel();

        }

        public void ExecuteRequestCommand(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/on-tour-request.png";
            CurrentChildView = new TourRequestViewModel();
      
        }

        /*public void ExecuteShowKeyPointsCommand(object obj)
        {
            /*if (obj is MainTouristView mainTouristView)
            {
                mainTouristView.MainFrame.Navigate(new FollowKeyPointsView());
            }
            CurrentChildView = new FollowKeyPointsViewModel();
        }
    */

        public void ExecuteRequestCreation(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentChildView = new TourRequestCreationViewModel();

        }


        public void ExecuteFollowKeyPoints(object tour)
        {
            if (tour is FollowingTourDTO followingTour)
            {
                // Use followingTour object to set properties or perform actions
                CurrentMarkerIconSource = "/Resources/Images/on marker.png";
                CurrentHomeIconSource = "/Resources/Images/home.png";
                CurrentUserIconSource = "/Resources/Images/tourist.png";
                CurrentNotificationIconSource = "/Resources/Images/bell.png";
                CurrentRequestIconSource = "/Resources/Images/tour-request.png";

                // Example of setting CurrentChildView to a new TourRequestCreationViewModel
                CurrentChildView = new FollowKeyPointsViewModel(followingTour);
            }

        }

        public void ExecuteRateTour(object tour)
        {
            if (tour is FollowingTourDTO followingTour)
            {
                // Use followingTour object to set properties or perform actions
                CurrentMarkerIconSource = "/Resources/Images/on marker.png";
                CurrentHomeIconSource = "/Resources/Images/home.png";
                CurrentUserIconSource = "/Resources/Images/tourist.png";
                CurrentNotificationIconSource = "/Resources/Images/bell.png";
                CurrentRequestIconSource = "/Resources/Images/tour-request.png";

                // Example of setting CurrentChildView to a new TourRequestCreationViewModel
                CurrentChildView = new RateTourViewModel(followingTour);
            }

        }


        public void ExecuteTourRequestView(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/on-tour-request.png";

            CurrentChildView = new TourRequestViewModel();
        }

        public void ExecuteSavedTourRequestView(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/on-tour-request.png";

            CurrentChildView = new SavedTourRequestViewModel();
        }

        public void ExecuteSingleTourView(object tourRequest)
        {
            if (tourRequest is TouristRequestDTO request)
            {
                // Use followingTour object to set properties or perform actions
                CurrentMarkerIconSource = "/Resources/Images/marker.png";
                CurrentHomeIconSource = "/Resources/Images/on home.png";
                CurrentUserIconSource = "/Resources/Images/tourist.png";
                CurrentNotificationIconSource = "/Resources/Images/bell.png";
                CurrentRequestIconSource = "/Resources/Images/tour-request.png";

                // Example of setting CurrentChildView to a new TourRequestCreationViewModel
                CurrentChildView = new SingleTourRequestViewModel(request);
            }

        }

        public void ExecuteSavedReservation(object obj)
        {
            CurrentMarkerIconSource = "/Resources/Images/on marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new SavedReservationViewModel();
        }


        public void ExecuteSearchCommand(string location,string language,int? duration)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new FilteredToursViewModel(location,language,duration);
        }


        public void ExecuteNoAvailableSpotsLeft(string location,int tourId)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new NoAvailableSpotsViewModel(location,tourId);
        }

        public void ExecuteBookTour(HomeTourDTO selectedTour,TourInstance? tourInstance) {

            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new BookTourViewModel(selectedTour,tourInstance);
        }


        public void ExecuteAlternativeTours(int location, int tourId)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/on home.png";
            CurrentUserIconSource = "/Resources/Images/tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new AlternativeLocationTourViewModel(location,tourId);
        }

        public void ExecuteComplexRequestCreation(PartViewModel part, ComplexTourRequestViewModel complexTourRequestViewModel)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            CurrentParts = complexTourRequestViewModel.Parts;


            CurrentChildView = new ComplexRequestCreationViewModel(part,complexTourRequestViewModel);
        }

        public void ExecuteGoBAckToComplexRequests(ComplexTourRequestViewModel complexTourRequestViewModel)
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            complexTourRequestViewModel.Parts = CurrentParts;
            CurrentChildView = complexTourRequestViewModel;

        }

        public void ExecuteSavedComplexTourRequest()
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";
            
            CurrentChildView = new SavedComplexTourRequestViewModel();
        }

        public void ExecuteSeeComplexTours()
        {
            CurrentMarkerIconSource = "/Resources/Images/marker.png";
            CurrentHomeIconSource = "/Resources/Images/home.png";
            CurrentUserIconSource = "/Resources/Images/on tourist.png";
            CurrentNotificationIconSource = "/Resources/Images/bell.png";
            CurrentRequestIconSource = "/Resources/Images/tour-request.png";

            CurrentChildView = new ShowAllComplexToursViewModel();
        }

        public void ExecuteSelectedComplexRequestView(object tourRequest)
        {

            if (tourRequest is TouristRequestDTO request)
            {
                CurrentMarkerIconSource = "/Resources/Images/marker.png";
                CurrentHomeIconSource = "/Resources/Images/home.png";
                CurrentUserIconSource = "/Resources/Images/on tourist.png";
                CurrentNotificationIconSource = "/Resources/Images/bell.png";
                CurrentRequestIconSource = "/Resources/Images/tour-request.png";

                CurrentChildView = new SelectedComplexRequestViewModel(request);
            }
        }


        public void ExecuteApprovedComplexRequest(object tourRequest)
        {

            if (tourRequest is ComplexTouristRequestDTO request)
            {
                CurrentMarkerIconSource = "/Resources/Images/marker.png";
                CurrentHomeIconSource = "/Resources/Images/home.png";
                CurrentUserIconSource = "/Resources/Images/on tourist.png";
                CurrentNotificationIconSource = "/Resources/Images/bell.png";
                CurrentRequestIconSource = "/Resources/Images/tour-request.png";

                CurrentChildView = new AcceptedComplexRequestViewModel(request);
            }
        }


        public void CheckForUniversalVoucher()
        {
           List<Tourist> tourists =  touristService.GetAll();

            foreach(Tourist t in tourists)
            {

                if(t.YearlyTourNumber >= 5)
                {
                    tourVoucherService.CreateUniversalTourVoucher(t);
                    t.YearlyTourNumber = 0;
                    touristService.Update(t);
                }

            }

        }


    }
}
