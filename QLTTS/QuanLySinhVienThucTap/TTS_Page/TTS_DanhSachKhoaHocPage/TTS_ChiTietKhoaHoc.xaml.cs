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

namespace QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachKhoaHocPage
{

    public partial class TTS_ChiTietKhoaHoc : Window
    {
        public TTS_ChiTietKhoaHoc(string MaKhoaDaoTao)
        {
            InitializeComponent();
            F1_TTS_ChiTietKhoaHocViewModel viewModel = new F1_TTS_ChiTietKhoaHocViewModel();
            viewModel.MaKhoaDaoTao = MaKhoaDaoTao;
            DataContext = viewModel;
            gridChiTietKhoaHoc.DataContext = viewModel;
        }
    }
}
