using QuanLySinhVienThucTap.UserControllerNEU;
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
    public partial class QuanLyNhanSu : Page
    {
        public QuanLyNhanSu(string PhongBanUser)
        {
            InitializeComponent();
            QuanLyNhanSuViewModel qlns = new QuanLyNhanSuViewModel();
            qlns.UserDepart = PhongBanUser;
            DataContext = qlns;
        }

        private void QuanLyNhanSu_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLyNhanSuViewModel viewModel)
            {
                viewModel.OnLoaded();
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ok");
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListDuAnItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }

    public class StatusToColorConverterOther : IValueConverter
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
            }
            else if (status == "expired")
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
