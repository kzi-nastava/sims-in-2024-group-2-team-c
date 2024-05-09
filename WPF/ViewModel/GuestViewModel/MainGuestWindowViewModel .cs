using BookingApp.Model;
using BookingApp.WPF.View.GuestView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuestViewModel
{
    public class MainGuestWindowViewModel : ViewModelBase
    {
        
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username)); // Obaveštenje da se promenila vrednost
                }
            }
        }
        

        // Konstruktor u kojem se postavlja Username
        public MainGuestWindowViewModel()
        {
            // Postavljanje Username na osnovu podataka iz baze ili drugog izvora
            Username = LoggedInUser.Username;
        }

    }
}