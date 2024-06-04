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
            set { _tourRequest = value; OnPropertyChanged(nameof(TourRequest)); }
        }
        private DateTime _selectedDate = new DateTime(2024,6, 5);
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (value > TourRequest.StartDate.Date && value < TourRequest.EndDate.Date)
                {
                    _selectedDate = value;
                }
                else
                {
                    MessageBox.Show("Selektuj turu izmedju " + TourRequest.StartDate.ToString("dd.MM.yyyy. HH:mm:ss") + " i " + TourRequest.EndDate.ToString("dd.MM.yyyy. HH:mm:ss"));
                }

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

        public ICommand AcceptCommand { get; private set; }
        public ICommand SendNotificationCommand { get; private set; }

        private readonly TourRequestNotificationService tourRequestNotificationService;
        private readonly TourRequestService tourRequestService;
        private readonly TourService tourService;
        private readonly TourReccommendationsService tourReccommendationsService;

        public AcceptingTourRequest_ViewModel()
        {
            IsSent = Visibility.Hidden;
            SendNotificationCommand = new ViewModelCommandd(SendNotification);
            AcceptCommand = new ViewModelCommandd(Accept);

            tourRequestService = new TourRequestService();
            tourService = new TourService();
            tourRequestNotificationService = new TourRequestNotificationService();
            tourReccommendationsService = new TourReccommendationsService();
        }

        private void Accept(object obj)
        {
            // Logika za prihvatanje zahteva
            MessageBox.Show("Zahtev prihvaćen!");
        }

        private void SendNotification(object obj)
        {
            string[] location = TourRequest.Location.Split(",");
            List<int> keyPoints = new List<int>();
            List<string> images = new List<string>();
            List<DateTime> dates = new List<DateTime>();
            dates.Add(SelectedDate);

            Tour tour = tourService.CreateTour(Name, location[0].Trim(), location[1].Trim(), Description, TourRequest.Language, 20, TourRequest.PeopleIds, dates, 2, images);
            TourRequestNotification notification = new TourRequestNotification
            {
                Id = tourRequestNotificationService.NextId(),
                RequestId = TourRequest.Id,
                TourId = tour.Id,
                TouristId = 4
                //TouristId = TourRequest.PeopleIds[0]
            };
            tourRequestNotificationService.SendNotification(notification);
            tourReccommendationsService.CheckForRecommendation(tour);
            IsSent = Visibility.Visible;
        }
    }
}
