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
    /// Interaction logic for BaoCaoKetQuaDaoTao.xaml
    /// </summary>
    public partial class BaoCaoKetQuaDaoTao : Page
    {
        public BaoCaoKetQuaDaoTao(string MaPhongBan)
        {
            InitializeComponent();
            BaoCaoKetQuaDaoTaoViewModel viewModel = new BaoCaoKetQuaDaoTaoViewModel();
            DataContext = viewModel;
            viewModel.MaPhongBan = MaPhongBan;
        }
    }
}
