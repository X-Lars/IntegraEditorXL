using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace IntegraEditorXL.Common.Converters
{
    public class MultiCompareConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
                return false;

            object firstValue = values[0];


            return values.All(x => x.Equals(firstValue));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
