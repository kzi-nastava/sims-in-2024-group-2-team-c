using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> imageNames)
            {
                List<string> fullImagePaths = new List<string>();
                string basePath = AppDomain.CurrentDomain.BaseDirectory; // Get the base directory of the application

                // Output the basePath to the console for debugging
                Console.WriteLine("Base Path: " + basePath);

                foreach (string imageName in imageNames)
                {
                    string fullPath = Path.Combine(basePath, "Resources", "Images", imageName);
                    fullImagePaths.Add(fullPath);
                }

                return fullImagePaths;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
