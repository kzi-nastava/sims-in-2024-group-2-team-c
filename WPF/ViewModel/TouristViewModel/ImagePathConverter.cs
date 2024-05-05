using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> imageNames)
            {
                List<string> fullImagePaths = new List<string>();
                foreach (string imageName in imageNames)
                {
                    string fullPath = $"D:\\Mila\\AHHHHHHHHHHHH\\sims-in-2024-group-2-team-c\\Resources\\Images\\{imageName}";
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
