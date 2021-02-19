using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Interface.Converters
{
    public class BooleanToVisibilityConverterExtension : MarkupExtension, IValueConverter
    {
        private BooleanToVisibilityConverter _converter;

        public BooleanToVisibilityConverterExtension() : base()
        {
            this._converter = new BooleanToVisibilityConverter();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this._converter.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this._converter.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
