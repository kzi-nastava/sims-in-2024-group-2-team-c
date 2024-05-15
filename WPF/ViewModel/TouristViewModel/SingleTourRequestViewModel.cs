using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class SingleTourRequestViewModel : ViewModelBase
    {

        private SelectedTourRequestDTO _tourRequest;

        public SelectedTourRequestDTO TourRequest
        {
            get { return _tourRequest; }
            set
            {
                _tourRequest = value;
                OnPropertyChanged(nameof(TourRequest));
            }
        }


        private DateTime _chosenDate;
        public DateTime ChosenDate
        {   get { return _chosenDate; }
            set
            {
                _chosenDate = value;
                OnPropertyChanged(nameof(ChosenDate));
            }


        }



        public ViewModelCommandd GoHomeCommand { get; }



        private readonly TourRequestNotificationService _notificationService;

        public ViewModelCommandd GoBackCommand { get; }

        private readonly MainViewModel _mainViewModel;

        public SingleTourRequestViewModel(TouristRequestDTO request) {

            _notificationService = new TourRequestNotificationService();
            _mainViewModel = LoggedInUser.mainViewModel;

            GoBackCommand = new ViewModelCommandd(ExecuteGoBackCommand);
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);

            LoadTourRequest(request);

        }

        private void LoadTourRequest(TouristRequestDTO request) {

            
        
           TourRequest = _notificationService.GetTourRequest(request);
            if (TourRequest.Status == "ACCEPTED")
            {
                ChosenDate = TourRequest.ChosenDate;

            }
        
        
        }

        public void ExecuteGoHome(object obj)
        {

            _mainViewModel.ExecuteShowTourCommand(obj);
        }

        public void ExecuteGoBackCommand(object obj)
        {

            _mainViewModel.ExecuteRequestCommand(obj);
        }



    }
}
