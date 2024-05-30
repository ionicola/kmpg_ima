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


namespace QuanLySinhVienThucTap.TTS_Page.TTS_QuanLyYeuCauPage
{
    /// <summary>
    /// Interaction logic for ThemYeuCau.xaml
    /// </summary>
    public partial class ThemYeuCau : Window
    {
        public ThemYeuCau(string TenTTS, string MaTTS)
        {
            InitializeComponent();
            F1_TTS_ThemYeuCauViewModel viewModel = new F1_TTS_ThemYeuCauViewModel();
            viewModel.TenTTS = TenTTS;
            viewModel.MaTTS = MaTTS;
            DataContext = viewModel;
        }
    }
}
