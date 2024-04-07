using BookingApp.Model;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationForm.xaml
    /// </summary>
    public partial class RegisterAccommodationForm : Window, INotifyPropertyChanged
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;
        private readonly LocationRepository locationRepository;
        List<string> imagePaths = new List<string>();


        public List<string> Images;

        public event PropertyChangedEventHandler PropertyChanged; 

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public RegisterAccommodationForm() //bez ovoga user
        {
            InitializeComponent();
            Title = "Register New Accommodation";
            DataContext = this;
            _repository = new AccommodationRepository();
            locationRepository = new LocationRepository();
            
        }


        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = true; 

            if (openFileDialog.ShowDialog() == true)
            {
                
                string[] selectedFiles = openFileDialog.FileNames;

                
                foreach (string fileName in selectedFiles)
                {
                    txtImagePath.Text += fileName + Environment.NewLine; 
                }
            }
        }


        private void SaveAccommodation(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string city = txtCity.Text.Trim().ToLower();
            Location location = locationRepository.GetLocationByCity(city);
            string type = txtType.Text.Trim();
            int maxGuests = int.Parse(txtMaxGuests.Text);
            int minBookingDays = int.Parse(txtMinBookingDays.Text);
            int cancellationDays = int.Parse(txtCancellationDays.Text);
            

            string[] paths = txtImagePath.Text.Split('|');

            
            foreach (string path in paths)
            {
      
                if (!string.IsNullOrEmpty(path))
                {
                    imagePaths.Add(path);
                }
            }


            Accommodation newAccommodation = new Accommodation()
            {
                Name = name,
                Location =location, 
                Type = type,
                MaxGuests = maxGuests,
                MinBookingDays = minBookingDays,
                CancellationDays = cancellationDays,
                Images = imagePaths
            };

            _repository.Save(newAccommodation);


            MessageBox.Show("Accommodation registered successfully.");

            Close();
        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
