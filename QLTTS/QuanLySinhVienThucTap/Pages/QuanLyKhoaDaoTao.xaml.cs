using QuanLySinhVienThucTap.Model;
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
    /// <summary>
    /// Interaction logic for QuanLyKhoaDaoTao.xaml
    /// </summary>
    public partial class QuanLyKhoaDaoTao : Page
    {
        public QuanLyKhoaDaoTaoViewModel qlkdt = new QuanLyKhoaDaoTaoViewModel();
        public QuanLyKhoaDaoTao(string PhongBanUser)
        {
            InitializeComponent();
            qlkdt.UserDepart = PhongBanUser;
            DataContext = qlkdt;
        }
        private void QuanLyKhoaDaoTao_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is QuanLyKhoaDaoTaoViewModel viewModel)
            {
                viewModel.OnLoaded(); // Gọi phương thức xử lý sự kiện Loaded trong ViewModel
            }
        }
        private void PheDuyet(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton?.DataContext is CacNhiemVuDaoTao item)
            {
                CacNhiemVuDaoTao NhiemVuDaoTao = item;

                qlkdt.SelectedNhiemVuPheDuyet = NhiemVuDaoTao;
            }
        }
    }
    public class TienDoToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.ToString() == "approved")
            {
                // Trả về nguồn ảnh của dấu tích xanh
                return new BitmapImage(new Uri("..\\Static\\Images\\Eo_circle_green_checkmark.svg.png", UriKind.RelativeOrAbsolute));
            }

            // Trả về nguồn ảnh mặc định hoặc null nếu không có dấu tích
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
