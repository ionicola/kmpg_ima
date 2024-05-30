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
    /// Interaction logic for QuanLyChung.xaml
    /// </summary>
    public partial class QuanLyChung : Page
    {
        public QuanLyChung(string PhongBanUser, string UserId)
        {
            InitializeComponent();
            QuanLyChungViewModel qlc = new QuanLyChungViewModel();
            qlc.UserDepart = PhongBanUser;
            qlc.UserId = UserId;
            DataContext = qlc;
        }
    }
}
