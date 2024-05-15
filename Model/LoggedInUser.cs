using BookingApp.WPF.View.GuestView;
using BookingApp.WPF.ViewModel.GuideViewModel;
using BookingApp.WPF.ViewModel.TouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class LoggedInUser
    {

        public static int Id { get; set; }

        public static string Username { get; set; }

        //public static string FirstName { get; set; }
        //public static string LastName { get; set; }
        //public static string Email { get; set; }

        public static string Role { get; set; }

        public static MainViewModel mainViewModel { get; set; }
        public static MainWindow_ViewModel mainGuideViewModel { get; set; }

        public static MainGuestWindow MainGuestWindow { get; set; }
        public static void Reset()
        {
            Id = 0;
            Username = null;
            Role = null;
            //FirstName = null;
            //LastName = null;
            //Email = null;
        }


    }
}
