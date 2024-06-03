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
    /// Interaction logic for AccommodationDetailsPage.xaml
    /// </summary>
    public partial class AccommodationDetailsPage : Page
    {
        public AccommodationDetailsPage()
        {
            InitializeComponent();

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

        private void BackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
