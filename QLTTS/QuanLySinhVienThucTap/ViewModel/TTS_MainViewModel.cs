using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages;
using QuanLySinhVienThucTap.TTS_Page;
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
    public class TTS_MainViewModel : BaseViewModel
    {
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DanhSachNhiemVu { get; set; }
        public ICommand DanhSachKhoaHoc { get; set; }
        public ICommand DiemDanhVaChamCong { get; set; }
        public ICommand KetQuaKhoaHoc { get; set; }
        public ICommand QuanLyYeuCau { get; set; }
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _userRole;
        public string UserRole
        {
            get { return _userRole; }
            set
            {
                if (_userRole != value)
                {
                    _userRole = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userDepart;

        public string UserDepart
        {
            get { return _userDepart; }
            set
            {
                if (_userDepart != value)
                {
                    _userDepart = value;
                    OnPropertyChanged();
                }
            }
        }
        public TTS_MainViewModel() {

            LoadedWindowCommand = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) =>
            {
                UserName = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == UserId).Select(x => x.TenTTS).SingleOrDefault();
                UserRole = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == UserId).Join(
                                                                                        DataProvider.Ins.DB.tblChucVus,
                                                                                        ql => ql.MaChucVu,
                                                                                        chucVu => chucVu.MaChucVu,
                                                                                        (ql, chucVu) => chucVu.TenChucVu
                                                                                         ).SingleOrDefault();
                UserDepart = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == UserId).Join(
                                                                                        DataProvider.Ins.DB.tblPhongBans,
                                                                                        ql => ql.MaPhongBan,
                                                                                        chucVu => chucVu.MaPhongBan,
                                                                                        (ql, chucVu) => chucVu.TenPhongBan
                                                                                         ).SingleOrDefault();
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new WelcomePage());
            });

            DanhSachNhiemVu = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) =>{
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_DanhSachNhiemVu(UserId));
            });
            DanhSachKhoaHoc = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) => {
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_DanhSachKhoaHoc(UserId));
            });
            DiemDanhVaChamCong = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) => {
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_DiemDanhVaChamCong(UserId));
            });
            KetQuaKhoaHoc = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) => {
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_KetQuaKhoaHoc(UserId));
            });
            QuanLyYeuCau = new RelayCommand<TTS_MainWindow>((p) => { return true; }, (p) => {
                Frame frame = p.gridMainTTS.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new TTS_Page.TTS_QuanLyYeuCau(UserId));
            });
        }
    }
}
