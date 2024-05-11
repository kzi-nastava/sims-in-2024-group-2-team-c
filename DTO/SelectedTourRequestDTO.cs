using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class SelectedTourRequestDTO
    {

        public int Number {  get; set; }
        public DateTime StartInterval { get; set; }
        public DateTime EndInterval { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Language { get; set; }

        public string Status { get; set; }
        
        public SelectedTourRequestDTO() { }

        public SelectedTourRequestDTO(int number,DateTime startInterval,DateTime endInterval,string description,string location,string language,string status)
        {
            Number = number;
            StartInterval = startInterval;
            EndInterval = endInterval;
            Description = description;
            Location = location;
            Language = language;
            Status = status;
        }


    }
}
