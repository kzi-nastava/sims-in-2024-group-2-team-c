﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class LoginDTO
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public LoginDTO() { }

        public LoginDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }


    }
}
