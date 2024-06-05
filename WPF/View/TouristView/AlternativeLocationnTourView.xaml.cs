﻿using BookingApp.DTO;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.View.TouristView
{
    /// <summary>
    /// Interaction logic for AlternativeLocationnTourView.xaml
    /// </summary>
    public partial class AlternativeLocationnTourView : Page
    {
        public AlternativeLocationnTourView()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


        private void ViewTour_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigateToNewPage(button);
        }


        private void NavigateToNewPage(Button button)
        {
            if (button?.DataContext is HomeTourDTO selectedTour)
            {

                SelectedTourViewModel selectedTourViewModel = new SelectedTourViewModel(selectedTour);
                SelectedTourView selectedTourView = new SelectedTourView();
                selectedTourView.DataContext = selectedTourViewModel;
                this.NavigationService.Navigate(selectedTourView);
            }
        }


    }
}
