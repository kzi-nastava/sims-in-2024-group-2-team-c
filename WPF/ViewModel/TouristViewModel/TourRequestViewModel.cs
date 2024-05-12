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
    public class TourRequestViewModel : ViewModelBase
    {

        private readonly TourRequestService tourRequestService;

        public ObservableCollection<TouristRequestDTO> _tourRequests;
        public ObservableCollection<TouristRequestDTO> TourRequests
        {
            get { return _tourRequests; }
            set
            {
                _tourRequests = value;
                OnPropertyChanged(nameof(TourRequests));
            }
        }


        public ViewModelCommandd NavigateCommand { get; }

        private readonly MainViewModel _mainViewModel;

        public TourRequestViewModel() {

            _mainViewModel = LoggedInUser.mainViewModel;

            tourRequestService = new TourRequestService();
            NavigateCommand = new ViewModelCommandd(ExecuteTourRequest);

            LoadTourRequests();
        
        
        }

        private void LoadTourRequests()
        {
            TourRequests = new ObservableCollection<TouristRequestDTO>(tourRequestService.GetTouristRequests());


        }

        private void ExecuteTourRequest(object tourRequest) {

            if (tourRequest is TouristRequestDTO request)
            {
                // Call the mainViewModel's function passing the tour
                _mainViewModel.ExecuteSingleTourView(request);
            }

        }


    }
}
