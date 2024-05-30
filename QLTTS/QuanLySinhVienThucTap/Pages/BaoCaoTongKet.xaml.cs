using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared;
using QuanLySinhVienThucTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for BaoCaoTongKet.xaml
    /// </summary>
    public partial class BaoCaoTongKet : Page
    {
        public BaoCaoTongKet(string MaPhongBan, string PhongBan, string User)
        {
            InitializeComponent();
            BaoCaoTongKetViewModel viewModel = new BaoCaoTongKetViewModel();
            DataContext = viewModel;
            viewModel.PhongBan = PhongBan;
            viewModel.MaPhongBan = MaPhongBan;
            viewModel.User = User;
        }
    }
}
