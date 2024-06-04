using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    /// 
   

    public partial class ReservationView : Window, INotifyPropertyChanged
    {
        
        private readonly LocationService locationService;
        private readonly TourReservationService tourReservationService;
        private readonly TourInstanceService tourInstanceService;
        private readonly TourService tourService;
        private readonly KeyPointService keyPointService;
        private readonly TourVoucherService tourVoucherService;
        private readonly PeopleInfoService peopleInfoService;


        private TourInstance _tourInstance;
        public TourInstance TourInstance
        {
            get { return _tourInstance; }
            set
            {
                if (_tourInstance != value)
                {
                    _tourInstance = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _location;

        public Location Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private Tour _tour;
        public Tour Tour
        {
            get { return _tour; }
            set
            {
                if (_tour != value)
                {
                    _tour = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TouristVoucherDTO> _vouchers;
        public ObservableCollection<TouristVoucherDTO> Vouchers
        {
            get { return _vouchers; }
            set
            {

                _vouchers = value;
                OnPropertyChanged();
                
            }

        }



        private List<TextBox[]> touristTextBoxes = new List<TextBox[]>();

        public ReservationView(TourInstance tourInstance, Tour tour)
        {
            InitializeComponent();

            tourReservationService = new TourReservationService();
            locationService = new LocationService();
            tourInstanceService = new TourInstanceService();
            tourService = new TourService();
            keyPointService = new KeyPointService();
            peopleInfoService = new PeopleInfoService();


            tourVoucherService = new TourVoucherService();
            TourInstance = tourInstance;
            Tour = tour;
            
            Location = locationService.Get(tour.LocationId);
           // Vouchers = new ObservableCollection<TouristVoucherDTO>(tourVoucherService.GetVouchersByTourId(tour.Id));


            

            DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NUmberOfPeople_Click(object sender, RoutedEventArgs e)
        {

            int turistNUmber = int.Parse(numberOfPeopleText.Text);
            int remainingSpots = TourInstance.MaxTourists - TourInstance.ReservedTourists;

            HandleRemainingSpots(turistNUmber, remainingSpots);

            numberOfPeopleText.Text = "";


        }

        private void HandleRemainingSpots(int turistNUmber, int remainingSpots)
        {
            if (turistNUmber > (TourInstance.MaxTourists - TourInstance.ReservedTourists))
            {
                remainingSpotsText.Text = $"Not enough available spots. Only {remainingSpots} spots left.";
            }
            else
            {
                GenerateTouristForms(turistNUmber);
                remainingSpotsText.Text = $"Remaining spots: {remainingSpots}";
            }
        }

        private void GenerateTouristForms(int numOfPeople)
        {
            // Clear any existing forms
            TouristFormsPanel.Children.Clear();

            // Generate forms for each tourist
            for (int i = 0; i < numOfPeople; i++)
            {
                var touristForm = CreateTouristForm(i + 1); // Start index from 1
                TouristFormsPanel.Children.Add(touristForm);
            }

            SaveButton.Visibility = Visibility.Visible;
        }

        

        private StackPanel CreateTouristForm(int index)
        {
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;


            var nameTextBox = new TextBox();
            var lastNameTextBox = new TextBox();
            var ageTextBox = new TextBox();

            // Set width for each TextBox

            nameTextBox.Width = 60;
            lastNameTextBox.Width = 60;
            ageTextBox.Width = 60;

          
            AddToStackPanel(index, stackPanel, nameTextBox, lastNameTextBox, ageTextBox);

            
            TextBox[] textBoxes = new TextBox[] { nameTextBox, lastNameTextBox, ageTextBox };
            touristTextBoxes.Add(textBoxes);

            return stackPanel;
        }

        private static void AddToStackPanel(int index, StackPanel stackPanel, TextBox nameTextBox, TextBox lastNameTextBox, TextBox ageTextBox)
        {
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} First Name:" });
            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} Last Name:" });
            stackPanel.Children.Add(lastNameTextBox);
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} Age:" });
            stackPanel.Children.Add(ageTextBox);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }



        public List<int> RetrievePeoplesIds()
        {

            List<int> ids = new List<int>();

            GetPeopleIds(ids);

            return ids;
        }

        private void GetPeopleIds(List<int> ids)
        {
            for (int i = 0; i < touristTextBoxes.Count; i++)
            {
                string firstName, lastName, ageText;
                GetInformation(i, out firstName, out lastName, out ageText);

                int? age = int.TryParse(ageText, out int parsedAge) ? parsedAge : 0;

                PeopleInfo onePerson = new PeopleInfo(firstName, lastName, age, false);
                peopleInfoService.Save(onePerson);
                ids.Add(onePerson.Id);

            }
        }

        private void GetInformation(int i, out string firstName, out string lastName, out string ageText)
        {
            TextBox[] textBoxes = touristTextBoxes[i];

            firstName = textBoxes[0].Text;
            lastName = textBoxes[1].Text;
            ageText = textBoxes[2].Text;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            List<int> peopleIds = RetrievePeoplesIds();
            
            int num = peopleIds.Count();
       
            int idInstance = TourInstance.Id;

            UpdateKeyPoints(peopleIds);

            TourReservation reservation = new TourReservation(idInstance, num, LoggedInUser.Id,peopleIds);

            tourReservationService.Save(reservation);

            TourInstance.ReservedTourists += num;
            tourInstanceService.Update(TourInstance);
            this.Close();

        }

        private void UpdateKeyPoints(List<int> peopleIds)
        {
            Tour tour = tourService.GetById(TourInstance.IdTour);

            List<KeyPoint> keyPoints = keyPointService.GetKeypointsByIds(tour.KeyPointIds);


            foreach (KeyPoint keyPoint in keyPoints)
            {

                foreach (int id in peopleIds)
                {
                    keyPoint.PeopleIds.Add(id);
                }

                keyPointService.Update(keyPoint);
            }

            

        }
    }
}
