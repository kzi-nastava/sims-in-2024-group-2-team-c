using BookingApp.DTO;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class AcceptedComplexRequestViewModel : ViewModelBase
    {

        private ComplexTouristRequestDTO _tourRequest;

        public ComplexTouristRequestDTO TourRequest
        {
            get { return _tourRequest; }
            set
            {
                _tourRequest = value;
                OnPropertyChanged(nameof(TourRequest));
            }
        }

        public TourRequestDTO _fullRequest;
        public TourRequestDTO FullRequest
        {
            get { return _fullRequest; }
            set
            {
                _fullRequest = value;
                OnPropertyChanged(nameof(FullRequest));
            }
        }

        private readonly TouristComplexTourService touristComplexTourService;

        public AcceptedComplexRequestViewModel(ComplexTouristRequestDTO request) {
            TourRequest = request;
            touristComplexTourService = new TouristComplexTourService();
            LoadFullRequest();
        }


        public void LoadFullRequest()
        {
            FullRequest =  touristComplexTourService.LoadRequest(TourRequest.TourRequestId);

        }
    }
}
