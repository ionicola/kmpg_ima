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
    /// Interaction logic for ThemDuAn.xaml
    /// </summary>
    public partial class ThemDuAn : Window
    {
        public ThemDuAn(string UserDepart)
        {
            InitializeComponent();
            ThemDuAnViewModel ViewModel = new ThemDuAnViewModel();
            DataContext = ViewModel;
            ViewModel.UserDepart = UserDepart;
        }
    }
}
