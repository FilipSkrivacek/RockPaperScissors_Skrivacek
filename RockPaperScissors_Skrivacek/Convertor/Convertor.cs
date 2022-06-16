using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace RockPaperScissors_Skrivacek.Convertor
{
    internal class Convertor : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;
            switch (number)
            {
                case 1:
                    return Application.Current.MainWindow.FindResource("rock");
                case 2:
                    return Application.Current.MainWindow.FindResource("paper");
                case 3:
                    return Application.Current.MainWindow.FindResource("scissors");
                default:
                    return new BitmapImage();
            }
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
