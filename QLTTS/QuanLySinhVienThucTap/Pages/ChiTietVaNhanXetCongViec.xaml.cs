using QuanLySinhVienThucTap.ViewModel;
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

namespace QuanLySinhVienThucTap.Pages
{
    public partial class ChiTietVaNhanXetCongViec : Page
    {
        public ChiTietVaNhanXetCongViec(string PhongBanUser)
        {
            InitializeComponent();
            ChiTietVaNhanXetCongViecVM ctnxcv = new ChiTietVaNhanXetCongViecVM();
            ctnxcv.UserDepart = PhongBanUser;
            DataContext = ctnxcv;
        }
        private void ChiTietVaNhanXetCongViec_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChiTietVaNhanXetCongViecVM viewModel)
            {
                viewModel.OnLoaded();
            }
        }
/*        private void RatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
*//*            if (DataContext is ChiTietVaNhanXetCongViecVM viewModel)
            {
                viewModel.DanhGia = System.Convert.ToInt32(e.NewValue);
            }*//*
        }*/
    }
/*    public class RatingValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value);
        }
    }*/

    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;

            if (status == "done")
            {
                return new SolidColorBrush(Colors.DarkGreen);
            }
            else if (status == "in-progress")
            {
                return new SolidColorBrush(Colors.DarkGoldenrod);
            } else if (status == "expired")
            {
                return new SolidColorBrush(Colors.DarkRed);
            }

            return new SolidColorBrush(Colors.DarkGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
