using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace IntegraEditorXL.Common.Converters
{
    public class RowIndexConverter : MarkupExtension, IValueConverter
    {
        static RowIndexConverter Converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataGridRow row = value as DataGridRow;

            if(row != null)
            {
                return row.GetIndex();
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if(Converter == null)
                Converter = new RowIndexConverter();

            return Converter;
        }
    }
}
