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

namespace QuanLySinhVienThucTap.Pages
{
    /// <summary>
    /// Interaction logic for BaoCaoSoBuoiLamViec.xaml
    /// </summary>
    public partial class BaoCaoSoBuoiLamViec : Page
    {
        public BaoCaoSoBuoiLamViec(string MaPhongBan)
        {
            InitializeComponent();
            BaoCaoSoBuoiLamViecViewModel viewModel = new BaoCaoSoBuoiLamViecViewModel();
            DataContext = viewModel;
            viewModel.MaPhongBan = MaPhongBan;
        }
    }
}
