using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace BookingApp.DTO
{
    public class HomeTourDTO
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        

        public List<BitmapImage> Images { get; set; }



        public HomeTourDTO()
        {
            Images = new List<BitmapImage>();
        }

        public HomeTourDTO(int tourId, string name, string location, List<string> imageNames)
        {
            TourId = tourId;
            Name = name;
            Location = location;
            Images = new List<BitmapImage>();

            foreach (string imageName in imageNames)
            {
                string baseImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");
                // string imagePath = Path.Combine("D:\\Mila\\AHHHHHHHHHHHH\\sims-in-2024-group-2-team-c\\Resources\\Images\\", imageName);
                // string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images", imageName);
                string path = Directory.GetCurrentDirectory();
                Console.WriteLine(path);
                //string imagePath = Path.Combine(@"..\\Resources\\Images\\", imageName);

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
                    Images.Add(bitmap);
                }
                else
                {
                    // Handle missing image file
                }
            }
        }


    }
}
