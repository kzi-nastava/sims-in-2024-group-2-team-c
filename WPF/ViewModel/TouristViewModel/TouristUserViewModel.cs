using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class TouristUserViewModel : ViewModelBase
    {

        private readonly MainViewModel _mainViewModel;

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {

                _username = value;
                OnPropertyChanged(nameof(Username));
                


            }
        }


        public TouristUserViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            Username = LoggedInUser.Username;
        }
    }
}
