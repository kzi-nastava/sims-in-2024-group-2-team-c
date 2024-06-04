using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public String Location { get; set; }
        public int NumOfComments { get; set; }

        public string ForumComment { get; set; }

        public string Username { get; set; }

        public bool HasBeenVisited { get; set; }

        public bool IsForumVeryUseful { get; set; }
    }
}
