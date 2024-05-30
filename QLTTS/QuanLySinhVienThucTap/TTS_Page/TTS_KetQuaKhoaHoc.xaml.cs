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

namespace QuanLySinhVienThucTap.TTS_Page
{
    public partial class TTS_KetQuaKhoaHoc : Page
    {
        public TTS_KetQuaKhoaHoc(string UserId)
        {
            InitializeComponent();
            TTS_KetQuaKhoaHocViewModel viewModel = new TTS_KetQuaKhoaHocViewModel();
            viewModel.UserId = UserId.ToUpper();
            DataContext = viewModel;
        }
    }
}
