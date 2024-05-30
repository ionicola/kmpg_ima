using MaterialDesignThemes.Wpf;
using QuanLySinhVienThucTap.UserControllerNEU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLySinhVienThucTap.Pages;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages.BaoCaoTongKetPage;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static bool isLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand QuanLyNhanSu { get; set; }
        public ICommand QuanLyChung { get; set; }
        public ICommand ChiTietVaNhanXetCongViec { get; set; }
        public ICommand QuanLyKhoaDaoTao { get; set; }
        public ICommand KiemTraKetQuaDaoTao { get; set; }
        public ICommand YeuCauChoPheDuyet { get; set; }
        public ICommand YeuCauDaPheDuyet { get; set; }
        public ICommand BaoCaoSoBuoiLamViec { get; set; }
        public ICommand BaoCaoKetQuaDaoTao { get; set; }
        public ICommand BaoCaoTienDoCongViec { get; set; }
        public ICommand BaoCaoTongKet { get; set; }
        public ICommand SelectAllCommand { get; set; }
        public ICommand CurrentPage { get; set; }
        public ICommand XuatBaoCaoTongKet { get; set; }

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

        public string PhongBanUser;
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                UserName = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserId).Select(x => x.TenQL).SingleOrDefault();
                UserRole = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserId).Join(
                                                                                        DataProvider.Ins.DB.tblChucVus,
                                                                                        ql => ql.MaChucVu,
                                                                                        chucVu => chucVu.MaChucVu,
                                                                                        (ql, chucVu) => chucVu.TenChucVu 
                                                                                         ).SingleOrDefault();
                UserDepart = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserId).Join(
                                                                                        DataProvider.Ins.DB.tblPhongBans,
                                                                                        ql => ql.MaPhongBan,
                                                                                        phongban => phongban.MaPhongBan,
                                                                                        (ql, phongBan) => phongBan.TenPhongBan  
                                                                                         ).SingleOrDefault();
                PhongBanUser = DataProvider.Ins.DB.tblQLs
                .Where(x => x.MaQL == UserId)
                .Select(x => x.MaPhongBan)
                .SingleOrDefault();

                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new WelcomePage());

            });

            QuanLyNhanSu = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new QuanLyNhanSu(PhongBanUser));
            });

            QuanLyChung = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new QuanLyChung(PhongBanUser, UserId));
            });

            ChiTietVaNhanXetCongViec = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new ChiTietVaNhanXetCongViec(PhongBanUser));
            });

            QuanLyKhoaDaoTao = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new QuanLyKhoaDaoTao(PhongBanUser));
            });

            YeuCauChoPheDuyet = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new YeuCauChoPheDuyet(PhongBanUser, UserId));
            });

            YeuCauDaPheDuyet = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new YeuCauDaPheDuyet(PhongBanUser,UserId));
            });

            BaoCaoKetQuaDaoTao = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new BaoCaoKetQuaDaoTao(PhongBanUser));
            });

            BaoCaoSoBuoiLamViec = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new BaoCaoSoBuoiLamViec(PhongBanUser));
            });

            BaoCaoTienDoCongViec = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new BaoCaoTienDoCongViec(PhongBanUser));
            });

            BaoCaoTongKet = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new BaoCaoTongKet(PhongBanUser,UserDepart, UserName));
            });

            XuatBaoCaoTongKet = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridQuanLy.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new XuatBaoCaoTongKet());
            });
        }
    }
        
}