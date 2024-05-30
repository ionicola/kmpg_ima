using QuanLySinhVienThucTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for AddTTSToDuAn.xaml
    /// </summary>
    public partial class AddTTSToDuAn : Window
    {
        public AddTTSToDuAnViewModel ViewModel { get; set; }
        public AddTTSToDuAn(string maDuAn, string UserDepart)
        {
            InitializeComponent();
            ViewModel = new AddTTSToDuAnViewModel();
            DataContext = ViewModel;
            ViewModel.maDuAn = maDuAn;
            ViewModel.UserDepart = UserDepart;
        }
    }
}
