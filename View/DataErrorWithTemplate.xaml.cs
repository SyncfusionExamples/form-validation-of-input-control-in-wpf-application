using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Validation_sample
{
    /// <summary>
    /// Interaction logic for View_using_IDataErrorInfo_CustomErrorTemplate.xaml
    /// </summary>
    public partial class DataErrorWithTemplate : UserControl
    {
        public DataErrorWithTemplate()
        {
            InitializeComponent();
        }
    }
    /// <summary>
    /// Convertor for empty string to visibility
    /// </summary>
    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts into visibility
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="targetType"> target type</param>
        /// <param name="parameter">input parameter</param>
        /// <param name="language">input language</param>
        /// <returns>converted value as object</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is string && string.IsNullOrEmpty(value.ToString()))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Converts the value back into the object
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="targetType"> target type</param>
        /// <param name="parameter">input parameter</param>
        /// <param name="language">input language</param>
        /// <returns>converted value as object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Convertor for String to visibility
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts to visibility
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="targetType">target type</param>
        /// <param name="parameter">input parameter</param>
        /// <param name="language">input language</param>
        /// <returns>converted value as object</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
       {
            if (value is string && !string.IsNullOrEmpty(value.ToString()))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Converts the value back into the object
        /// </summary>
        /// <param name="value">input value</param>
        /// <param name="targetType"> target type</param>
        /// <param name="parameter">input parameter</param>
        /// <param name="language">input language</param>
        /// <returns>converted value as object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            throw new NotImplementedException();
        }
    }

}
