using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.DTO
{
    public class ActiveTourDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        //public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string ActiveKeyPoint { get; set; }
        //public List<int> KeyPoints { get; set; }
        public int Duration { get; set; }
        //public List<string> Images { get; set; }

        //public List<BitmapImage> BitmapImages { get; set; }

        public ActiveTourDTO() { }
    }
}
