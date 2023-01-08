using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ApprovalToolForRumikApp.Converters
{
    public class StringToIntWithValidation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int temp;
            if (int.TryParse(value.ToString(), out temp))
                return temp;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "" || !value.ToString().All(char.IsDigit))
                return 0.0;
                       
            return value.ToString();
        }
    }
}
