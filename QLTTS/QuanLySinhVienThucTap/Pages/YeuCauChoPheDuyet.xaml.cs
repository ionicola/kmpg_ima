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
    /// Interaction logic for YeuCauChoPheDuyet.xaml
    /// </summary>
    public partial class YeuCauChoPheDuyet : Page
    {
        public YeuCauChoPheDuyet(string PhongBanUser, string UserId)
        {
            InitializeComponent();
            YeuCauChoPheDuyetViewModel yccpd = new YeuCauChoPheDuyetViewModel();
            yccpd.UserDepart = PhongBanUser;
            yccpd.UserId = UserId;
            DataContext = yccpd;
        }
    }
}
