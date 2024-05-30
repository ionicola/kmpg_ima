using QuanLySinhVienThucTap.ViewModel;
using System;
using System.Collections.Generic;
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

namespace QuanLySinhVienThucTap.TTS_Page
{
    public partial class TTS_DanhSachNhiemVu : Page
    {
        public TTS_DanhSachNhiemVu(string UserId)
        {
            InitializeComponent();
            TTS_DanhSachNhiemVuViewModel viewModel = new TTS_DanhSachNhiemVuViewModel();
            viewModel.UserId = UserId.ToUpper();
            DataContext = viewModel;
        }
        private void TTS_DanhSachNhiemVu_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is TTS_DanhSachNhiemVuViewModel viewModel)
            {
                viewModel.LoadDuAnObj(); // Gọi phương thức xử lý sự kiện Loaded trong ViewModel
            }
        }
    }
}
