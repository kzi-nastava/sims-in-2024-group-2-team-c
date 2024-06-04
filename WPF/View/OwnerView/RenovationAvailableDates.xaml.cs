using BookingApp.Model;
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



namespace BookingApp.WPF.View.OwnerView
{
    /// <summary>
    /// Interaction logic for RenovationAvailableDates.xaml
    /// </summary>
    public partial class RenovationAvailableDates : Page
    {
        private RenovationAvailableDatesViewModel viewModel;
        public RenovationAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int duration)
        {
            InitializeComponent();

            viewModel = new RenovationAvailableDatesViewModel(selectedAccommodation, startDate, endDate, duration);
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


        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\RenovationsList.xaml", UriKind.RelativeOrAbsolute));
        }

        private void OwnerWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Proverite koji taster je pritisnut
            switch (e.Key)
            {
                case Key.RightCtrl:
                    SubmitButton_Click(null, null);
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
