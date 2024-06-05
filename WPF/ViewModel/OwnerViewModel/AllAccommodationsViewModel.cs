using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace BookingApp.WPF.ViewModel.OwnerViewModel
{
    public class AllAccommodationsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Accommodation> _accommodations;
        private AccommodationRepository _accommodationService;
      
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                _accommodations = value;
                OnPropertyChanged();
            }
        }

        public AllAccommodationsViewModel()
        {
            _accommodationService = new AccommodationRepository();
            LoadAccommodations();
            
        }

        public void NavigateToDetails(Accommodation accommodation)
        {
            var detailsPage = new AccommodationDetailsPage();
            detailsPage.DataContext = accommodation;

            // Use MainWindow for navigation
            NavigationService navigationService = NavigationService.GetNavigationService(Application.Current.MainWindow);
            if (navigationService != null)
            {
                navigationService.Navigate(detailsPage);
            }
            else
            {
                MessageBox.Show("Navigation service is not available.");
            }
        }

        private void LoadAccommodations()
        {
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
