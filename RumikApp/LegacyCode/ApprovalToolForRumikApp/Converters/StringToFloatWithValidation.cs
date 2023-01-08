using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ApprovalToolForRumikApp.Converters
{
    class StringToFloatWithValidation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float temp;
            if (float.TryParse(value.ToString(), out temp))
                return temp;

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "")
                return 0.0;

            var regex = new Regex(@"^-?[0-9,\.]+$");

            if (!regex.IsMatch(value.ToString()))
                return 0.0;

            return value.ToString();
        }
    }
}
