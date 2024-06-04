using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using BookingApp.WPF.View.GuideView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class ComplexTourRequest_ViewModel : ViewModelBase
    {
        //private readonly TourRequestService _tourRequestService;
        private readonly ComplexTourRequestService _complexTourRequestService;
       // private readonly RequestForComplexTourService _requestForComplexTourService;
       // private readonly MainWindow_ViewModel mainView;
        private ObservableCollection<TourRequest> _tourRequests;
        public ObservableCollection<TourRequest> TourRequests
        {
            get { return _tourRequests; }
            set { _tourRequests = value; OnPropertyChanged(nameof(TourRequests)); }
        }

        private TourRequest _selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get { return _selectedTourRequest; }
            set
            {
                if (_selectedTourRequest != value)
                {
                    _selectedTourRequest = value;
                    OnPropertyChanged(nameof(_selectedTourRequest));
                }
            }

        }

        //public ViewModelCommandd AcceptTourRequestCommand { get; }
        public ICommand AcceptTourRequestCommand { get; }
        public ComplexTourRequest_ViewModel()
        {
           // mainView = LoggedInUser.mainGuideViewModel;
           // _tourRequestService = new TourRequestService();
           // _requestForComplexTourService = new RequestForComplexTourService();
            _complexTourRequestService = new ComplexTourRequestService();
            LoadRequests();
            AcceptTourRequestCommand = new ViewModelCommandd(AcceptRequest);
        }
        public void LoadRequests()
        {
            //TourRequests = new ObservableCollection<TourRequestDTO>(_tourRequestService.GetOnHoldRequests());
            TourRequests = new ObservableCollection<TourRequest>(_complexTourRequestService.GetOnHoldRequests());
        }
        /*public void LoadDatePicker()
        {
            DateTime? selectedStartDate = SelectedStartDate;
            DateTime? selectedEndDate = SelectedEndDate;
            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                DateTime startDate = selectedStartDate.Value;
                DateTime endDate = selectedEndDate.Value;
            }
        }*/
        public void AcceptRequest(object obj)
        {

            if (SelectedTourRequest != null)
            {
                var unavailableSlots = _complexTourRequestService.GetUnAvailableTimeSlots(SelectedTourRequest.Id, LoggedInUser.Id);
                List<DateTime> availableSlots = _complexTourRequestService.GetAvailableTimeSlots(SelectedTourRequest.StartDate, SelectedTourRequest.EndDate, unavailableSlots);
                AcceptTourWindow acceptTourWindow = new AcceptTourWindow(availableSlots, SelectedTourRequest);
                acceptTourWindow.Show();
                LoadRequests();
                    /*if (acceptTourWindow.ShowDialog() == true)
                    {
                        var selectedTimeSlot = acceptTourWindow.SelectedTimeSlot;
                        _complexTourRequestService.AcceptTourPart(SelectedTourRequest.Id, LoggedInUser.Id, selectedTimeSlot);
                        MessageBox.Show("Tour part accepted.");
                    }*/



                //bool canAccept = _tourRequestService.AcceptRequest(SelectedTourRequest);
                /*if (canAccept == true)
                    LoadRequests();*/
                // MessageBox.Show("Ok");
            }
            /*else
            {
                MessageBox.Show("Select");
            }*/
        }
    }
}
