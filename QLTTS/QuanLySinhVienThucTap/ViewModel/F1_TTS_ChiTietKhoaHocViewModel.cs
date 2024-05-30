using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachKhoaHocPage;
using QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachKhoaHocPage.TTS_ChiTietKhoaHocPage_Inside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class F1_TTS_ChiTietKhoaHocViewModel : BaseViewModel
    {
        private string _MaKhoaDaoTao;

        public string MaKhoaDaoTao
        {
            get { return _MaKhoaDaoTao; }
            set
            {
                _MaKhoaDaoTao = value;
                OnPropertyChanged();
            }
        }
        private string _TenKhoaHoc;

        public string TenKhoaHoc
        {
            get { return _TenKhoaHoc; }
            set
            {
                _TenKhoaHoc = value;
                OnPropertyChanged();
            }
        }

        public int VideoNo = 0;
        public ICommand LoadWindowCommand { get; set; }

        public F1_TTS_ChiTietKhoaHocViewModel()
        {
            LoadWindowCommand = new RelayCommand<TTS_ChiTietKhoaHoc>((p) => { return true; }, (p) =>
            {
                TenKhoaHoc = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(x=> x.MaKhoaDaoTao == MaKhoaDaoTao).Select(x => x.TenKhoa).SingleOrDefault();
                Frame frame = p.gridChiTietKhoaHoc.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_NoiDungVideoKhoaHoc(MaKhoaDaoTao, VideoNo));
            });
        }
    }
}
