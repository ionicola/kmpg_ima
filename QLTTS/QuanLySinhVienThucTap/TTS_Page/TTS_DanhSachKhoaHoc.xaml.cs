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
    /// <summary>
    /// Interaction logic for TTS_DanhSachKhoaHoc.xaml
    /// </summary>
    public partial class TTS_DanhSachKhoaHoc : Page
    {
        public TTS_DanhSachKhoaHoc(string UserId)
        {
            InitializeComponent();
            TTS_DanhSachKhoaHocViewModel viewModel = new TTS_DanhSachKhoaHocViewModel();
            viewModel.UserId = UserId.ToUpper();
            DataContext = viewModel;
        }
    }
}
