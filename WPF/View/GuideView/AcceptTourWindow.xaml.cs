﻿using BookingApp.Model;
using BookingApp.WPF.ViewModel.GuideViewModel;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideView
{
    /// <summary>
    /// Interaction logic for AcceptTourWindow.xaml
    /// </summary>
    public partial class AcceptTourWindow : Window
    {
        public AcceptTourWindow(List<DateTime> availableSlots, TourRequest request)
        {
            InitializeComponent();
            DataContext = new AcceptTour_ViewModel(availableSlots, request);
        }
    }
}
