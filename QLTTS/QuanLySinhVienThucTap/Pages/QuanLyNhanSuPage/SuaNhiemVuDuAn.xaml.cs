using QuanLySinhVienThucTap.Model;
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

namespace QuanLySinhVienThucTap.Pages.QuanLyNhanSuPage
{
    /// <summary>
    /// Interaction logic for SuaNhiemVuDuAn.xaml
    /// </summary>
    public partial class SuaNhiemVuDuAn : Window
    {
        public SuaNhiemVuDuAnViewModel ViewModel { get; set; }
        public SuaNhiemVuDuAn(NhiemVuCacDuAn SelectedItem)
        {
            InitializeComponent();
            ViewModel = new SuaNhiemVuDuAnViewModel();
            DataContext = ViewModel;
            ViewModel.SelectedItemDA = SelectedItem;
        }
    }
}
