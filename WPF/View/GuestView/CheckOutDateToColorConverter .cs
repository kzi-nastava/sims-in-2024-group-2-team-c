using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuestView
{
    public class CheckOutDateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime checkOutDate && checkOutDate < DateTime.Now)
            {
                return new SolidColorBrush(Color.FromArgb(50, 255, 0, 0)); // Prozirno crvena boja
            }

            return Brushes.Transparent; // Ako datum nije prošao, koristi transparentnu boju
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
