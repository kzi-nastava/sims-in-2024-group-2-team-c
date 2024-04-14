using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ActiveTourKeyPointDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }

        public ActiveTourKeyPointDTO() { }

        public ActiveTourKeyPointDTO(string name, string description, string active)
        {
            Name = name;
            Description = description;
            Active = active;


        }



    }
}
