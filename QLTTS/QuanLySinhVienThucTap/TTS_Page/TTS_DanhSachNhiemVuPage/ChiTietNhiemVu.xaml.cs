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
using System.Windows.Shapes;

namespace QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachNhiemVuPage
{
    /// <summary>
    /// Interaction logic for ChiTietNhiemVu.xaml
    /// </summary>
    public partial class ChiTietNhiemVu : Window
    {
        public ChiTietNhiemVu(int maNhiemVuDA)
        {
            InitializeComponent();
            F1_TTS_ChiTietNhiemVuViewModel viewModel = new F1_TTS_ChiTietNhiemVuViewModel();
            DataContext = viewModel;
            viewModel.MaNhiemVuDA = maNhiemVuDA;
        }
    }
}
