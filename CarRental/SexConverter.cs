using System;
using System.Globalization;
using System.Windows.Data;
using Converter = System.Convert;

namespace CarRental
{
    class SexConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }

            if(Converter.ToInt32(value) == 0)
            {
                return "Male";
            }

            return "Female";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value.ToString() == "Male")
            {
                return 0;
            }

            return 1;
        }
    }
}
