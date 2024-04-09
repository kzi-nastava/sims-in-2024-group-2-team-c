using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    
    public enum UserRole
    {
        tourist,
        guide,
        owner,
        guest
    }
    


    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; }
        public bool Super { get; set; }

       

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password,Role.ToString(),Super.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            if (Enum.TryParse<UserRole>(values[3], out UserRole role))
            {
                Role = role;
            }
            Super = Convert.ToBoolean(values[4]);
        }
    }
}
