using BookingApp.Model;
using BookingApp.Service.TourServices;
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
        private readonly TouristService _touristService;

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

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {

                _name = value;
                OnPropertyChanged(nameof(Name));



            }
        }


        private string _lastName;
        public string LastName
        {

            get { return _lastName; }
            set
            {

                _lastName = value;
                OnPropertyChanged(nameof(LastName));



            }
        }


        public TouristUserViewModel() {
            _mainViewModel = LoggedInUser.mainViewModel;
            Username = LoggedInUser.Username;

            _touristService = new TouristService();

            Name = _touristService.GetFirstName(LoggedInUser.Id);
            LastName = _touristService.GetLastName(LoggedInUser.Id);


        }
    }
}
