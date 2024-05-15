using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BookingApp.Converters
{
    public class FirstImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<string> imagePaths && imagePaths.Count > 0)
            {
                string firstImagePath = imagePaths[0];
                if (File.Exists(firstImagePath))
                {
                    return new BitmapImage(new Uri(firstImagePath));
                }
            }

            // If no image or file not found, return null
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
