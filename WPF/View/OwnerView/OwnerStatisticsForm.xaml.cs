using BookingApp.Model;
using BookingApp.WPF.ViewModel.OwnerViewModel;
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

namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerStatisticsForm.xaml
    /// </summary>
    public partial class OwnerStatisticsForm : Page
    {
        private OwnerStatisticsViewModel view;
        public OwnerStatisticsForm() 
        {
            InitializeComponent();
            view = new OwnerStatisticsViewModel(); 
            DataContext = view;
        }

        public void Button_Click(object sender , RoutedEventArgs e)
        {
            view.RefreshStatistics();
        }

        private void AccommodationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is Accommodation selectedAccommodation)
            {
                view.SelectedAccommodation = selectedAccommodation;
            }
        }

    }
}
