using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    class LoggedInUser
    {

        public static int Id { get; set; }

        public static string Username { get; set; }

        //public static string FirstName { get; set; }
        //public static string LastName { get; set; }
        //public static string Email { get; set; }

        public static string Role { get; set; }

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
