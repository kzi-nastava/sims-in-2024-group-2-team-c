using System;
using System.Collections.Generic;
using System.IO;
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

        public TourDTO(string name, string language, int duration)
        {

            Name = name;
            Description = string.Empty;
            Language = language;
            Duration = duration;
            KeyPointIds = new List<int>();
            Images = new List<string>();
            ViewLocation = string.Empty;
            BitmapImages = new List<BitmapImage>();
            //Images = imageNames.Split(',').ToList();

            /*foreach (string imageName in Images)
            {
                string baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");
                // string imagePath = Path.Combine("D:\\Mila\\AHHHHHHHHHHHH\\sims-in-2024-group-2-team-c\\Resources\\Images\\", imageName);
                // string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", imageName);
                //string imagePath = Path.Combine("\\Resources\\Images\\", imageName);

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Find the index of the substring "\bin\Debug\net6.0-windows\"
                int index = baseDirectory.IndexOf("\\bin\\Debug\\net6.0-windows\\", StringComparison.OrdinalIgnoreCase);

                // Remove the substring "\bin\Debug\net6.0-windows\" from the base directory
                string parentDirectory = baseDirectory.Remove(index);

                // Construct the new path
                string newPath = Path.Combine(parentDirectory, "Resources", "Images", imageName);

                // Use the new path
                string imagePath = newPath;
                if (File.Exists(imagePath))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                    BitmapImages.Add(bitmap);
                }
                else
                {
                    // Handle missing image file
                }
                
            }*/
        }
    }
}
