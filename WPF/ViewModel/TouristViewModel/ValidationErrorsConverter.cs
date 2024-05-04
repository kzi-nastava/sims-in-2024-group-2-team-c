using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ValidationErrorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Collections.IEnumerable errors)
            {
                var firstError = errors.Cast<object>().FirstOrDefault();
                if (firstError != null)
                {
                    if (firstError is ValidationError validationError)
                    {
                        return validationError.ErrorContent?.ToString();
                    }
                    else
                    {
                        return firstError.ToString();
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
