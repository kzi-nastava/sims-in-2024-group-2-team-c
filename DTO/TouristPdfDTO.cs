using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.DTO
{
    public class TouristPdfDTO
    {


        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<int> KeyPointIds { get; set; }
        public int Duration { get; set; }

        public List<string> Images { get; set; }

        public DateTime Date { get; set; }


        public TouristPdfDTO(string name, string language, int duration,string location, string description,List<string> imageNames, DateTime date)
        {
            
            Name = name;
            Language = language;
            Duration = duration;
            Description = description;
            Location = location;
            Date = date;
            Images = imageNames;

            
        }





    }
}
