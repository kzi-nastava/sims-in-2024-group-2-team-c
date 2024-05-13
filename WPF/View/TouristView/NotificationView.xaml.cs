﻿using BookingApp.WPF.ViewModel.TouristViewModel;
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
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Page
    {

        private readonly NotificationViewModel viewModel;
        public NotificationView()
        {
            InitializeComponent();
            viewModel = new NotificationViewModel();
            DataContext = viewModel;
        }

        

        private void Reccommendations_Click(object sender, RoutedEventArgs e)
        {
           viewModel.ShowRecommendations();
        }

        private void AcceptedRequests_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowAcceptedRequests();
        }

        private void TouristAdded_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ShowTouristsAdded();
        }

        private void ViewTourClick_Click(object sender, RoutedEventArgs e)
        {




        }
    }
}
