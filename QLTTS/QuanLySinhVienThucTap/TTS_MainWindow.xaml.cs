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
    public partial class TTS_MainWindow : Window
    {
        public TTS_MainWindow(string UserId)
        {
            InitializeComponent();
            TTS_MainViewModel viewModel = new TTS_MainViewModel();
            viewModel.UserId = UserId.ToUpper();
            DataContext = viewModel;
        }
    }
}
