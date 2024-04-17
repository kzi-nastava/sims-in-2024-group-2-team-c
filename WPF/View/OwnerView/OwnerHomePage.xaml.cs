using BookingApp.Commands;
using BookingApp.View;
using BookingApp.WPF.ViewModel.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for OwnerHomePage.xaml
    /// </summary>
    public partial class OwnerHomePage : Page
    {

        public OwnerHomePage()
        {
            InitializeComponent();
            
        }

        private ICommand _navigateToGuestRatingCommand;

        public ICommand NavigateToGuestRatingCommand
        {
            get
            {
                return _navigateToGuestRatingCommand ?? (_navigateToGuestRatingCommand = new RelayCommand(NavigateToGuestRating));
            }
        }

        private void NavigateToGuestRating()
        {
             this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\GuestRatingForm.xaml", UriKind.Relative));
        }

      /*  private void RateGuestsClick(object sender, RoutedEventArgs e)
        {
           
            this.NavigationService.Navigate(new Uri("WPF\\View\\OwnerView\\GuestRatingForm.xaml", UriKind.Relative));
        }*/

        
        
        
        
        
        
        
        
        
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
