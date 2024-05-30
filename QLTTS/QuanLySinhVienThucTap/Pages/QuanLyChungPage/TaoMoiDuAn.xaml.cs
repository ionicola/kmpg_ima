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

namespace QuanLySinhVienThucTap.Pages.QuanLyChungPage
{
    /// <summary>
    /// Interaction logic for TaoMoiDuAn.xaml
    /// </summary>
    public partial class TaoMoiDuAn : Window
    {

        public TaoMoiDuAn(string UserId)
        {
            InitializeComponent();
            TaoMoiDuAnViewModel viewModel = new TaoMoiDuAnViewModel();
            DataContext = viewModel;
            viewModel.UserId = UserId;
        }
    }
}
