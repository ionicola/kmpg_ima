using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLySinhVienThucTap.Pages.QuanLyNhanSuPage
{
    public partial class AddNhiemVuToDuAn : Window
    {
        public AddNhiemVuToDuAnViewModel ViewModel { get; set; }
        public AddNhiemVuToDuAn(string maDuAn, string maTTSNV, string TenTTSNV)
        {
            InitializeComponent();
            ViewModel = new AddNhiemVuToDuAnViewModel();
            DataContext = ViewModel;
            ViewModel.maDuAn = maDuAn;
            ViewModel.MaTTS = maTTSNV;
            ViewModel.TenTTS = TenTTSNV;
        }
    }
}
