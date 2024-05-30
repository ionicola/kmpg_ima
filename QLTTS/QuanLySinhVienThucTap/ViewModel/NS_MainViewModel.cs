using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.NS_Page;
using QuanLySinhVienThucTap.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class NS_MainViewModel : BaseViewModel
    {
        public static bool isLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand XuatBaoCao { get; set; }

        public ICommand BaoCaoLuong { get; set; }
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

        private string _userDepartDisplay;

        public string UserDepartDisplay
        {
            get { return _userDepartDisplay; }
            set
            {
                if (_userDepartDisplay != value)
                {
                    _userDepartDisplay = value;
                    OnPropertyChanged();
                }
            }
        }


        public string PhongBanUser;

        public NS_MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<NS_MainWindow>((p) => { return true; }, (p) =>
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
                UserDepartDisplay = "Hành chính Nhân sự";
                PhongBanUser = DataProvider.Ins.DB.tblQLs
                .Where(x => x.MaQL == UserId)
                .Select(x => x.MaPhongBan)
                .SingleOrDefault();

                Frame frame = p.gridBaoCao.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new WelcomePage());
            });

            XuatBaoCao = new RelayCommand<NS_MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridBaoCao.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new NS_BaoCaoChamCong(PhongBanUser,UserDepart,UserName)) ;
            });

            BaoCaoLuong = new RelayCommand<NS_MainWindow>((p) => { return true; }, (p) =>
            {
                Frame frame = p.gridBaoCao.Children[0] as Frame;
                while (frame.NavigationService.RemoveBackEntry() != null) { }
                frame.NavigationService.Navigate(new NS_BaoCaoLuong());
            });

        }
    }
}
