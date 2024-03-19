using BookingApp.Model;
using BookingApp.Repository;
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
        private TourInstance _tourInstance;
        private Tour _tour;
        private LocationRepository _locationRepository = new LocationRepository();
        private TouristRepository _touristRepository = new TouristRepository();
        private ReservationRepository _reservationRepository = new ReservationRepository();
        private TourInstanceRepository _tourInstanceRepository = new TourInstanceRepository();

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


        public ReservationView(TourInstance tourInstance, Tour tour)
        {
            InitializeComponent();
            
            TourInstance = tourInstance;
            Tour = tour;
            Location = _locationRepository.Get(tour.LocationId);
            DataContext = this;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NUmberOfPeople_Click(object sender, RoutedEventArgs e) {

            int turistNUmber = int.Parse(numberOfPeopleText.Text);
            int remainingSpots = TourInstance.MaxTourists - TourInstance.ReservedTourists;

            if (turistNUmber > (TourInstance.MaxTourists - TourInstance.ReservedTourists)) {
                remainingSpotsText.Text = $"Not enough available spots. Only {remainingSpots} spots left.";
            }
            else {
                GenerateTouristForms(turistNUmber);
                remainingSpotsText.Text = $"Remaining spots: {remainingSpots}";
            }
            
            numberOfPeopleText.Text = "";


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
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }



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


        }


        private void SaveButton_Click(object sender, RoutedEventArgs e) {


            List<string> usernames = ReadingUsernames();
            int num = usernames.Count();
            List<int> touristIds = _touristRepository.GetTouristIdsByUsernames(usernames);
            int idInstance = TourInstance.Id;

            Reservation reservation = new Reservation(idInstance,num,touristIds);

            _reservationRepository.Save(reservation);

            TourInstance.ReservedTourists += num;
            _tourInstanceRepository.Update(TourInstance);
            this.Close();//--------------------------------------

        }


    }
}
