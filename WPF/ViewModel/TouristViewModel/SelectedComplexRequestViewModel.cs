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
    public class SelectedComplexRequestViewModel : ViewModelBase
    {

        private TouristRequestDTO _tourRequest;

        public TouristRequestDTO TourRequest
        {
            get { return _tourRequest; }
            set
            {
                _tourRequest = value;
                OnPropertyChanged(nameof(TourRequest));
            }
        }


        private ObservableCollection<ComplexTouristRequestDTO> _complexRequests;

        public ObservableCollection<ComplexTouristRequestDTO> ComplexRequests
        {
            get { return _complexRequests; }
            set
            {
                _complexRequests = value;
                OnPropertyChanged(nameof(ComplexRequests));
            }
        }

        private readonly TouristComplexTourService _touristComplexService;

        public ViewModelCommandd NavigateCommand { get; }

        private readonly MainViewModel _mainViewModel;

        public SelectedComplexRequestViewModel(TouristRequestDTO request) {
            TourRequest = request;
            _touristComplexService = new TouristComplexTourService();
            NavigateCommand = new ViewModelCommandd(ExecuteTourRequest);
            _mainViewModel = LoggedInUser.mainViewModel;
            LoadPartsOfTour();
        }

        private void LoadPartsOfTour()
        {
            
            ComplexRequests = new ObservableCollection<ComplexTouristRequestDTO>(_touristComplexService.GetPartsByTourId(TourRequest.TourRequestId));
        }

        private void ExecuteTourRequest(object tourRequest)
        {

            if (tourRequest is ComplexTouristRequestDTO request)
            {
                // Call the mainViewModel's function passing the tour
                _mainViewModel.ExecuteApprovedComplexRequest(request);
            }

        }


    }
}
