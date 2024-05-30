using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.ViewModel;

namespace QuanLySinhVienThucTap.Pages
{
    /// <summary>
    /// Interaction logic for BaoCaoTienDoCongViec.xaml
    /// </summary>
    public partial class BaoCaoTienDoCongViec : Page
    {
        public BaoCaoTienDoCongViec(string MaPhongBan)
        {
            InitializeComponent();
            BaoCaoTienDoCongViecViewModel viewModel = new BaoCaoTienDoCongViecViewModel();
            DataContext = viewModel;
            viewModel.MaPhongBan = MaPhongBan;
        }
    }
}
