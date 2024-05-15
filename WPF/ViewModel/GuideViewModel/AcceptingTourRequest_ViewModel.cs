using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
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
    public class AcceptingTourRequest_ViewModel : ViewModelBase
    {
        private TourRequestDTO _tourRequest;
        public TourRequestDTO TourRequest
        {
            get { return _tourRequest; }
            set { _tourRequest= value; OnPropertyChanged(nameof(TourRequest)); }
        }
        private DateTime _selectedDate = new DateTime(2024, 1, 1);
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
        private Visibility _isSent;
        public Visibility IsSent
        {
            get { return _isSent; }
            set { _isSent = value; OnPropertyChanged(nameof(IsSent)); }
        }


        public ViewModelCommandd SendNotificationCommand;
        private readonly TourRequestNotificationService tourRequestNotificationService;
        private readonly TourRequestService tourRequestService;
        private readonly TourService tourService;

        public AcceptingTourRequest_ViewModel()
        {
            IsSent = Visibility.Hidden;
            SendNotificationCommand = new ViewModelCommandd(SendNotification);
            tourRequestService = new TourRequestService();
            tourService = new TourService();
            tourRequestNotificationService = new TourRequestNotificationService();
            /*if(SelectedDate!=null && SelectedDate < TourRequest.StartDate.Date && SelectedDate > TourRequest.EndDate.Date)
            {
                MessageBox.Show("Selektuj turu izmedju "+ TourRequest.StartDate.ToString("dd.MM.yyyy. HH:mm:ss") + "i "+ TourRequest.EndDate.ToString("dd.MM.yyyy. HH:mm:ss"));
            }*/
        }

        private void SendNotification(object obj)
        {
            string[] location = TourRequest.Location.Split(",");
            List<int> keyPoints = new List<int>();
            List<string> images = new List<string>();
            List<DateTime> dates = new List<DateTime>();
            dates.Add(SelectedDate);
            TourRequestNotification notification = new TourRequestNotification 
            {
                Id = tourRequestNotificationService.NextId(),
                RequestId= TourRequest.Id,
                TourId = 5,
                TouristId = 4
                //TouristId = TourRequest.PeopleIds[0]
            };
            tourService.CreateTour(Name, location[0], location[1], Description, TourRequest.Language, 20, TourRequest.PeopleIds, dates, 2, images);
            tourRequestNotificationService.SendNotification(notification);
            IsSent = Visibility.Visible;
        }
    }
}
