using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;


namespace BookingApp.Model
{
    public class GuestRating : ISerializable
    {
        public int Id { get; set; }
        public Guest Guest { get; set; }
       
        public int Cleanliness { get; set; }
        public int RuleRespecting { get; set; } 
        public string Comment { get; set; }
        public DateTime RatingDate { get; set; }

        public GuestRating()
        {
            // Inicijalizacija datuma ocene
            RatingDate = DateTime.Now;
        }

        public string[] ToCSV()  
        {
            string[] csvValues = { Id.ToString(), Guest.Id.ToString(), Cleanliness.ToString(), RuleRespecting.ToString(), Comment, RatingDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest = new Guest() { Id = Convert.ToInt32(values[1]) };
            Cleanliness = Convert.ToInt32(values[2]);
            RuleRespecting  = Convert.ToInt32(values[3]);
            Comment = values[4];
            RatingDate = Convert.ToDateTime(values[5]);
        }

        
    }
}
