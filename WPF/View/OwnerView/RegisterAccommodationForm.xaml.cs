using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationForm.xaml
    /// </summary>
    public partial class RegisterAccommodationForm : Page
    {
        public RegisterAccommodationViewModel viewModel;
        public RegisterAccommodationForm() 
        {
            InitializeComponent();
            viewModel = new RegisterAccommodationViewModel();
            DataContext = viewModel;
            this.Loaded += BasePage_Loaded;
            App.StaticPropertyChanged += OnAppPropertyChanged;
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

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            viewModel.BrowseImage_Click(sender, e);
        }

        private void SaveAccommodation(object sender, RoutedEventArgs e)
        {
            viewModel.SaveAccommodation(sender, e);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }


    }
}
