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

namespace QuanLySinhVienThucTap.Pages.QuanLyKhoaDaoTaoPage
{
    public partial class ThemNhiemVuDaoTao : Window
    {
        public ThemNhiemVuDaoTaoViewModel viewModel = new ThemNhiemVuDaoTaoViewModel();
        public ThemNhiemVuDaoTao(string PhongBan, string MaKhoaDaoTao)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.PhongBan = PhongBan;
            viewModel.SelectedKhoaDaoTao = MaKhoaDaoTao;

        }
    }
}
