using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class BookTourViewModel : ViewModelBase
    {

        private int? _touristNumber;

        public int? TouristNumber
        {
            get { return _touristNumber; }
            set
            {
                _touristNumber = value;
                OnPropertyChanged(nameof(TouristNumber));
            }
        }


        private string _availableSpots;
        public string AvailableSpots
        {
            get { return _availableSpots; }
            set { _availableSpots = value; 
                    OnPropertyChanged(nameof(AvailableSpots));
            }


        }


        private HomeTourDTO _selectedTour;


        public HomeTourDTO SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }


        private TourInstance _tourInstance;

        public TourInstance TourInstance
        {
            get { return _tourInstance; }
            set
            {
                _tourInstance = value;
                OnPropertyChanged(nameof(TourInstance));
            }
        }


        private TourDTO _tour;

        public TourDTO Tour
        {
            get { return _tour; }
            set
            {
                _tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }


        private ObservableCollection<PeopleInfo> _people;
        public ObservableCollection<PeopleInfo> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));


            }
        }


        private List<PeopleInfo> _peopleList;
        public List<PeopleInfo> PeopleList
        {
            get { return _peopleList; }
            set
            {
                _peopleList = value;
                OnPropertyChanged(nameof(PeopleList));

            }
        }

        private ObservableCollection<TourVoucher> _vouchers;
        public ObservableCollection<TourVoucher> Vouchers
        {
            get { return _vouchers; }
            set
            {

                _vouchers = value;
                OnPropertyChanged(nameof(Vouchers));

            }

        }

        private bool _isReservationApproved;
        public bool IsReservationApproved
        {
            get { return _isReservationApproved; }
            set
            {
                _isReservationApproved = value;
                OnPropertyChanged(nameof(IsReservationApproved));
            }
        }

        private TourVoucher _selectedVoucher;
        public TourVoucher SelectedVoucher
        {
            get { return _selectedVoucher; }
            set
            {
                _selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }


        private readonly TourReservationService tourReservationService;
        private readonly TourVoucherService tourVoucherService;
        private readonly TourInstanceService tourInstanceService;

        public ViewModelCommandd CheckCommand { get; }
        public ViewModelCommandd BookCommand { get; }

        public BookTourViewModel(HomeTourDTO selectedTour, TourInstance instance) {
            tourReservationService = new TourReservationService();
            tourVoucherService = new TourVoucherService();
            tourInstanceService = new TourInstanceService();    
            SelectedTour = selectedTour;
            TourInstance = instance;
            Tour = tourReservationService.makeTourDTO(selectedTour.TourId);
            People = new ObservableCollection<PeopleInfo>();
            PeopleList = new List<PeopleInfo>();
            CheckCommand = new ViewModelCommandd(CheckPeopleNumber);
            BookCommand = new ViewModelCommandd(BookTour);
            LoadVouchers();
            IsReservationApproved = false;
        }

        private void UpdateRequestCreatable()
        {
            IsReservationApproved = true;
        }

        private void LoadVouchers()
        {
            Vouchers = new ObservableCollection<TourVoucher>(tourVoucherService.GetVouchersByTourId(SelectedTour.TourId));
        }

        private void CheckPeopleNumber(object obj) {

            int remainingSpots = TourInstance.MaxTourists - TourInstance.ReservedTourists;
            HandleRemainingSpots(TouristNumber, remainingSpots);

        }

        private void HandleRemainingSpots(int? touristNumber, int remainingSpots)
        {
            if (touristNumber > (TourInstance.MaxTourists - TourInstance.ReservedTourists))
            {
                AvailableSpots = $"Not enough available spots. Only {remainingSpots} spots left.";
            }
            else
            {
                GenerateTouristForms(touristNumber);
                AvailableSpots = $"Remaining spots: {remainingSpots}";
                UpdateRequestCreatable();
            }
        }


        private void GenerateTouristForms(int? touristNumber)
        {
            
                People.Clear(); // Clear existing people
                for (int i = 0; i < touristNumber; i++)
                {
                    // Add a new person with empty details
                    People.Add(new PeopleInfo());


                }
           
        }

        private void BookTour(object obj)
        {


            tourVoucherService.Delete(SelectedVoucher);
            TourInstance.ReservedTourists = TourInstance.ReservedTourists + (int)TouristNumber;
            tourInstanceService.Update(TourInstance);


            foreach (var person in People)
            {
                PeopleList.Add(person);
            }

            tourReservationService.SaveTouristReservation(TourInstance.Id, TouristNumber, LoggedInUser.Id,PeopleList);


            People.Clear();

        }


    }
}
