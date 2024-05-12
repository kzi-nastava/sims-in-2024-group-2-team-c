using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourRequestDTO
    {
        public int Id { get; set; }
        public TourRequestStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int GuideId { get; set; }
        public List<int> PeopleIds { get; set; }
        public string Language { get; set; }
        public int? NumberOfPeople { get; set; }
        public string Description { get; set; }

        public TourRequestDTO() { }
    }
}
