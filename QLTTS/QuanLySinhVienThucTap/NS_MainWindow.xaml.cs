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

namespace QuanLySinhVienThucTap
{
    /// <summary>
    /// Interaction logic for NS_MainWindow.xaml
    /// </summary>
    public partial class NS_MainWindow : Window
    {
        public NS_MainWindow(string id)
        {
            InitializeComponent();
            NS_MainViewModel viewModel = new NS_MainViewModel();
            viewModel.UserId = id.ToUpper();
            DataContext = viewModel;
        }
    }
}
