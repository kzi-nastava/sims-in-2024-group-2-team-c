using BookingApp.Model;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class MainGuestWindowViewModel : ViewModelBase
    {
        private readonly System.Windows.Navigation.NavigationService _navigationService;
        private readonly MainGuestWindow _mainGuestWindow;

        public ICommand NavigateToGuestProfilViewCommand { get; private set; }
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        
        public MainGuestWindowViewModel(System.Windows.Navigation.NavigationService navigationService, MainGuestWindow mainGuestWindow)
        {
            Username = LoggedInUser.Username;
            NavigateToGuestProfilViewCommand = new ViewModelCommand<object>(NavigateToGuestProfilView);
            _navigationService = navigationService;
            _mainGuestWindow = mainGuestWindow;
        }


        private void NavigateToGuestProfilView(object parameter)
        {
            if (_navigationService != null)
            {
                _mainGuestWindow.ChangeHeaderText("User informations");
                _navigationService.Navigate(new GuestProfilView());
            }
        }

    }
}