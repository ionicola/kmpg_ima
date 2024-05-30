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

namespace QuanLySinhVienThucTap.NS_Page
{
    /// <summary>
    /// Interaction logic for NS_BaoCaoChamCong.xaml
    /// </summary>
    public partial class NS_BaoCaoChamCong : Page
    {
        public NS_BaoCaoChamCong(string MaPhongBan, string PhongBan, string User)
        {
            InitializeComponent();
            NS_BaoCaoChamCongViewModel viewModel = new NS_BaoCaoChamCongViewModel();
            DataContext = viewModel;
            viewModel.PhongBan = PhongBan;
            viewModel.MaPhongBan = MaPhongBan;
            viewModel.User = User;
        }
    }
}
