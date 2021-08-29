using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace IntegraEditorXL.Common.Converters
{
    public class ComparisonVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts the comparison result of two objects to a <see cref="bool"/>.
        /// </summary>
        /// <param name="value">An <see cref="object"/> providing the value.</param>
        /// <param name="targetType"><i>Unused parameter.</i></param>
        /// <param name="parameter">An <see cref="object"/> provided as converter parameter to compare to.</param>
        /// <param name="culture"><i>Unused parameter.</i></param>
        /// <returns>An <see cref="object"/> representing a <see cref="bool"/> containing true if the <paramref name="value"/> equals the <paramref name="parameter"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(parameter) == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
