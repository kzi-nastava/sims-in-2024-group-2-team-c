﻿using BookingApp.DTO;
using BookingApp.Repository;
using BookingApp.Service.TourServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TourStatisticView.xaml
    /// </summary>
    public partial class TourStatisticView : Window
    {
        private ObservableCollection<TourStatisticDTO> _endedTours;
        public ObservableCollection<TourStatisticDTO> EndedTours
        {
            get { return _endedTours; }
            set
            {
                if (_endedTours != value)
                {
                    _endedTours = value;
                }
            }
        }
        private TourStatisticDTO _tour;
        public TourStatisticDTO SelectedTour
        {
            get { return _tour; }
            set
            {
                if (_tour != value)
                {
                    _tour = value;
                }
            }
        }
        private readonly TourService _tourService;
        private readonly EndedToursService _endedToursService;

        public TourStatisticView()
        {
            InitializeComponent();
            DataContext = this;
            _tourService = new(new TourRepository());
            _endedToursService = new();
            EndedTours = new ObservableCollection<TourStatisticDTO>();
            tourView.IsEnabled = true;
            LoadEndedTours();
        }

        private void LoadEndedTours()
        {
            EndedTours = new ObservableCollection<TourStatisticDTO>(_endedToursService.GetEndedTours());
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                MessageBox.Show($"Max Tourists: {SelectedTour.MaxTourists}\n" +
                                $"Reserved Tourists: {SelectedTour.ReservedTourists}\n" 
                               /* $"Present Tourists: {SelectedTour.PresentTourists}"*/,
                                "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please select a tour.", "Tour Statistics", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TourView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedTour = (TourStatisticDTO)tourView.SelectedItem;
        }
        /*public TourStatisticView()
        {
            InitializeComponent();
        }*/
    }
}
