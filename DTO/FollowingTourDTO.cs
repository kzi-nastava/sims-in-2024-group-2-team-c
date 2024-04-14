using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class FollowingTourDTO
    {

        public string Name { get; set; }
        public bool Active {  get; set; }

        public int TourId{ get; set; }
        public int TourInstanceId {  get; set; }

      public FollowingTourDTO() { }

        public FollowingTourDTO(string name,bool active,int tourId,int tourInstanceId) {
            Name = name;
            Active = active;
            TourId = tourId;
            TourInstanceId = tourInstanceId;
        
        }


    }
}
