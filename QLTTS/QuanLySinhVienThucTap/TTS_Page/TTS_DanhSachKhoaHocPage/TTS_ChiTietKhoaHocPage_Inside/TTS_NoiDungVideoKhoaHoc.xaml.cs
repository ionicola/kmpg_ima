using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachKhoaHocPage.TTS_ChiTietKhoaHocPage_Inside
{
    /// <summary>
    /// Interaction logic for TTS_NoiDungVideoKhoaHoc.xaml
    /// </summary>
    public partial class TTS_NoiDungVideoKhoaHoc : Page
    {
        private string pathInDatabase;
        public TTS_NoiDungVideoKhoaHoc(string MaKhoaDaoTao, int VideoNo)
        {
            InitializeComponent();
            F2_TTS_NoiDungVideoKhoaHocViewModel viewModel = new F2_TTS_NoiDungVideoKhoaHocViewModel();
            DataContext = viewModel;
            viewModel.MaKhoaDaoTao = MaKhoaDaoTao;
            pathInDatabase = DataProvider.Ins.DB.tblNoiDungDaoTaos.Where(x => x.MaKhoaDaoTao == MaKhoaDaoTao).Select(x => x.Video).SingleOrDefault();

            string videoPath = $"..\\..\\VideoFileSystem\\VideoKhoaHoc\\{pathInDatabase}";
            mediaElement.Source = new Uri(videoPath, UriKind.RelativeOrAbsolute);
            Loaded += TTS_NoiDungVideoKhoaHoc_Loaded;
        }

        private void TTS_NoiDungVideoKhoaHoc_Loaded(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

    }

}