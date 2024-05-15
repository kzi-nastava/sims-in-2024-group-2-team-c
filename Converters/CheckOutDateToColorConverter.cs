using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.Converters
{
    public class CheckOutDateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime checkOutDate && checkOutDate < DateTime.Now)
            {
                return new SolidColorBrush(Color.FromArgb(50, 0, 0, 255)); // Svetlo plava
            }

            return Brushes.Transparent; // Ako datum nije prošao, koristi transparentnu boju
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
