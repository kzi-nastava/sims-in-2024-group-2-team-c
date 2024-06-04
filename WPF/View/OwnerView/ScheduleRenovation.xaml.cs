﻿using BookingApp.Model;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for ScheduleRenovation.xaml
    /// </summary>
    public partial class ScheduleRenovation : Page
    {
        private RenovationViewModel viewModel;
        public ScheduleRenovation()
        {
            InitializeComponent();
            viewModel = new RenovationViewModel();
            DataContext = viewModel;
            this.Loaded += BasePage_Loaded;
            App.StaticPropertyChanged += OnAppPropertyChanged;
            this.KeyDown += OwnerWindow_KeyDown;

        }
        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            SetLanguage();
        }

        private void OnAppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(App.CurrentLanguage))
            {
                SetLanguage();
            }
        }
        private void SetLanguage()
        {
            string currentLanguage = App.CurrentLanguage;
            var newResource = new ResourceDictionary
            {
                Source = new Uri(currentLanguage, UriKind.Relative)
            };

            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newResource);
        }



        private void SaveRenovation(object sender, RoutedEventArgs e)
        {
            Accommodation selectedAccommodation = (Accommodation)accommodationsComboBox.SelectedItem;

            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.MinValue;

            int duration = 0;
            int.TryParse(Duration.Text, out duration);

            if(duration < 0)
            {
                MessageBox.Show("Duration must be longer");
                return;
            }

            if (selectedAccommodation == null || startDate == null || endDate == null || Duration.Text == "")
            {
                MessageBox.Show("Please fill in all of the fields");
                return;
            }

            if (endDate < startDate)
            {
                MessageBox.Show("End date can not be before start date");
                return;
            }

            viewModel.SaveRenovation(selectedAccommodation, startDate, endDate, duration);
            //this.NavigationService.Navigate(new Uri("WPF/View/OwnerView/RenovationAvailableDates.xaml?selectedAccommodationId=" + selectedAccommodation.Id, UriKind.Relative));
            NavigationService.Navigate(new RenovationAvailableDates(selectedAccommodation, startDate, endDate, duration));
        
        }
        private void OwnerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Proverite koji taster je pritisnut
            switch (e.Key)
            {
                case Key.RightCtrl:
                    SaveRenovation(null, null);
                    break;
                default:
                    break;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Postavite fokus na stranicu
            this.Focus();
        }
    }
}
