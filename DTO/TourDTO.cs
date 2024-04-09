using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BookingApp.DTO
{
    public class TourDTO
    {
        public string Name { get; set; }
        public int LocationId { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public List<int> KeyPointIds { get; set; }
        public int Duration { get; set; }
        public List<string> Images { get; set; }

        public List<BitmapImage> BitmapImages { get; set; }

        public string ViewLocation { get; set; }

        public TourDTO()
        {

            Name = string.Empty;
            Description = string.Empty;
            Language = string.Empty;
            KeyPointIds = new List<int>();
            Images = new List<string>();
            ViewLocation = string.Empty;
            BitmapImages = new List<BitmapImage>();
        }
    }
}
