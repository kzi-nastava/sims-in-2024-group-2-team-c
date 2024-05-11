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

        public ViewModelCommandd GoHomeCommand { get; }



        private readonly TourRequestService _tourRequestService;

        public ViewModelCommandd GoBackCommand { get; }

        private readonly MainViewModel _mainViewModel;

        public SingleTourRequestViewModel(TouristRequestDTO request) {

            _tourRequestService = new TourRequestService();
            _mainViewModel = LoggedInUser.mainViewModel;

            GoBackCommand = new ViewModelCommandd(ExecuteGoBackCommand);
            GoHomeCommand = new ViewModelCommandd(ExecuteGoHome);

            LoadTourRequest(request);

        }

        private void LoadTourRequest(TouristRequestDTO request) {
        
           TourRequest = _tourRequestService.GetTourRequest(request);
        
        
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
