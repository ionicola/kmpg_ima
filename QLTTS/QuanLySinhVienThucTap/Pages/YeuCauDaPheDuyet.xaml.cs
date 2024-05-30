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
    /// Interaction logic for YeuCauDaPheDuyet.xaml
    /// </summary>
    public partial class YeuCauDaPheDuyet : Page
    {
        public YeuCauDaPheDuyet(string PhongBanUser, string UserId)
        {
            InitializeComponent();
            YeuCauDaPheDuyetViewModel ycdpd = new YeuCauDaPheDuyetViewModel();
            ycdpd.UserDepart = PhongBanUser;
            ycdpd.UserId = UserId;
            DataContext = ycdpd;
        }
    }
}
