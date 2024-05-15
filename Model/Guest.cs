using System;
using System.Runtime.Serialization;

namespace BookingApp.Model
{
    public class Guest : User 
    { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool SuperGuestStatus { get; set; }
        public int BonusPoints { get; set; }

        public Guest() { }

        public Guest(string username, string password,string name,string surname, string email, string phonenumber, bool superGuest, int bonusPoints)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phonenumber;
            SuperGuestStatus = superGuest;
            BonusPoints = bonusPoints;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Name, Surname, Email, PhoneNumber, SuperGuestStatus.ToString(), BonusPoints.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Name = values[3];
            Surname = values[4];
            Email = values[5];
            PhoneNumber = values[6];
            SuperGuestStatus = Convert.ToBoolean(values[7]);
            BonusPoints = Convert.ToInt32(values[8]);
        }



    }
}