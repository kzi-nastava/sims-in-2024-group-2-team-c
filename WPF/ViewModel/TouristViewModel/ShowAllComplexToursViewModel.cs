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
    public class ShowAllComplexToursViewModel : ViewModelBase
    {

        private ObservableCollection<TouristRequestDTO> _tourRequests;
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

      
        private readonly ComplexTourRequestService _complexTourRequestService;
        private readonly RequestForComplexTourService requestForComplexTourService;

        public ShowAllComplexToursViewModel() {
            
            _mainViewModel = LoggedInUser.mainViewModel;

          
           NavigateCommand = new ViewModelCommandd(ExecuteTourRequest);
            _complexTourRequestService = new ComplexTourRequestService();
            requestForComplexTourService = new RequestForComplexTourService();
            RefreshTourRequests();

           LoadTourRequests();

        }

        public bool AreAllRequestsAccepted(List<int> requestIds)
        {
            // Assuming TourRequest has a Status property and Accepted is a status value
          return  requestForComplexTourService.CheckingStatusOfRequests(requestIds);
          
        }

        private void RefreshTourRequests()
        {
            List<ComplexTourRequest> complexRequests = _complexTourRequestService.GetAll();
            List<TourRequest> requests = requestForComplexTourService.GetAll();
            DateTime now = DateTime.Now;

            DateTime limitDate = now.AddHours(48);
            

            foreach (var complexRequest in complexRequests)
            {
                bool IsHold = requestForComplexTourService.AreAllRequestsOnHold(complexRequest.TourRequestIds);
                int NumberOfRequests = complexRequest.TourRequestIds.Count;
                if (complexRequest.Status == ComplexTourRequestStatus.OnHold && IsHold) {

                    int FirstRequestid = complexRequest.TourRequestIds[0];
                    DateTime RequestDate = requestForComplexTourService.GetTimeOfFirstRequest(FirstRequestid);

                    TimeSpan timeDifference = RequestDate - now;
                    bool is48HoursAway = timeDifference.TotalHours >= 48;

                    if (!is48HoursAway)
                    {
                        complexRequest.Status = ComplexTourRequestStatus.Invalid;
                        _complexTourRequestService.Update(complexRequest);
                        
                    }
                }

                bool areAllAccepted = AreAllRequestsAccepted(complexRequest.TourRequestIds);

                if (areAllAccepted)
                {
                    complexRequest.Status = ComplexTourRequestStatus.Accepted;
                    _complexTourRequestService.Update(complexRequest);
                }



            }




        }

        public void LoadTourRequests()
        {
            TourRequests = new ObservableCollection<TouristRequestDTO>(_complexTourRequestService.GetTouristRequests());


        }

        private void ExecuteTourRequest(object tourRequest)
        {

            if (tourRequest is TouristRequestDTO request)
            {
                // Call the mainViewModel's function passing the tour
                _mainViewModel.ExecuteSelectedComplexRequestView(request);
            }

        }



    }
}
