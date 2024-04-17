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
            Vouchers = new ObservableCollection<TouristVoucherDTO>(tourVoucherService.GetVouchersByTourId(tour.Id));


            

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

        /* private StackPanel CreateTouristForm(int index)
         {
             var stackPanel = new StackPanel();
             stackPanel.Orientation = Orientation.Horizontal;




             var usernameLabel = new Label();
             usernameLabel.Content = $"Tourist {index} Username:";
             var usernameTextBox = new TextBox();
             usernameTextBox.Width = 60;


             var nameLabel = new Label();
             nameLabel.Content = $"Tourist {index} First Name:";
             var nameTextBox = new TextBox();
             nameTextBox.Width = 60;

             var lastNameLabel = new Label();
             lastNameLabel.Content = $"Tourist {index} Last Name:";
             var lastNameTextBox = new TextBox();
             lastNameTextBox.Width = 60;

             var ageLabel = new Label();
             ageLabel.Content = $"Tourist {index} Age:";
             var ageTextBox = new TextBox();
             ageTextBox.Width = 60;

             // Add more fields as needed (e.g., Age, Username)
             stackPanel.Children.Add(usernameLabel);
             stackPanel.Children.Add(usernameTextBox);
             stackPanel.Children.Add(nameLabel);
             stackPanel.Children.Add(nameTextBox);
             stackPanel.Children.Add(lastNameLabel);
             stackPanel.Children.Add(lastNameTextBox);
             stackPanel.Children.Add(ageLabel);
             stackPanel.Children.Add(ageTextBox);

             return stackPanel;
         }*/

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

            // Create labels and add TextBox controls to StackPanel
            
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} First Name:" });
            stackPanel.Children.Add(nameTextBox);
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} Last Name:" });
            stackPanel.Children.Add(lastNameTextBox);
            stackPanel.Children.Add(new Label { Content = $"Tourist {index} Age:" });
            stackPanel.Children.Add(ageTextBox);

            // Store the TextBox controls in an array and add the array to the list
            TextBox[] textBoxes = new TextBox[] { nameTextBox, lastNameTextBox, ageTextBox };
            touristTextBoxes.Add(textBoxes);

            return stackPanel;
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }


        /*
        private List<String> ReadingUsernames() {

            List<string> usernames = new List<string>();
            foreach (UIElement element in TouristFormsPanel.Children)
            {
                if (element is StackPanel)
                {
                    StackPanel stackPanel = (StackPanel)element;
                    foreach (UIElement child in stackPanel.Children)
                    {
                        if (child is TextBox)
                        {
                            TextBox textBox = (TextBox)child;
                            string labelText = ((Label)stackPanel.Children[stackPanel.Children.IndexOf(child) - 1]).Content.ToString();
                            if (labelText.Contains("Username"))
                            {
                                usernames.Add(textBox.Text);
                                
                            }

                        }
                    }


                }


            }
            return usernames;


        }*/


        public List<int> RetrievePeoplesIds()
        {

            List<int> ids = new List<int>();

            for (int i = 0; i < touristTextBoxes.Count; i++)
            {
                TextBox[] textBoxes = touristTextBoxes[i];

                // Retrieve values from TextBox controls
               
                string firstName = textBoxes[0].Text;
                string lastName = textBoxes[1].Text;
                string ageText = textBoxes[2].Text;

                
                int age = int.TryParse(ageText, out int parsedAge) ? parsedAge : 0;

                PeopleInfo onePerson = new PeopleInfo(firstName,lastName,age,false);
                peopleInfoService.Save(onePerson);
                ids.Add(onePerson.Id);

            }

            return ids;
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            List<int> peopleIds = RetrievePeoplesIds();
            //List<string> usernames = ReadingUsernames();
            int num = peopleIds.Count();
            //List<int> touristIds = _touristRepository.GetTouristIdsByUsernames(usernames);
            


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
