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

namespace QuanLySinhVienThucTap.Pages.QuanLyKhoaDaoTaoPage
{
    /// <summary>
    /// Interaction logic for ThemKhoaDaoTao.xaml
    /// </summary>
    public partial class ThemKhoaDaoTao : Window
    {
        public TaoMoiKhoaDaoTaoViewModel viewModel { get; set; }
        public ThemKhoaDaoTao(string userDepart)
        {
            InitializeComponent();
            viewModel = new TaoMoiKhoaDaoTaoViewModel();
            DataContext = viewModel;
            viewModel.PhongBan = userDepart;
        }
        private void myComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLoaiKhoa = myComboBox.SelectedItem as ComboBoxItemWithTag; // Thay YourItemType bằng kiểu dữ liệu của mục trong ItemsSource
            if (selectedLoaiKhoa != null)
            {
                viewModel.SelectedItemId = selectedLoaiKhoa.Tag;
                viewModel.GenMaKhoaDaoTao();
            }
        }
    }
}
