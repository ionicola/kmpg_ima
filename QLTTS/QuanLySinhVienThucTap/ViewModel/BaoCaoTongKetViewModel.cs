using QuanLySinhVienThucTap.Pages;
using QuanLySinhVienThucTap.Pages.BaoCaoTongKetPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class BaoCaoTongKetViewModel : BaseViewModel
    {
        private string _User;

        public string User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
            }
        }
        private string _PhongBan;

        public string PhongBan
        {
            get { return _PhongBan; }
            set
            {
                _PhongBan = value;
                OnPropertyChanged();
            }
        }
        private string _MaPhongBan;

        public string MaPhongBan
        {
            get { return _MaPhongBan; }
            set
            {
                _MaPhongBan = value;
                OnPropertyChanged();
            }
        }

        private DateTime _NgayBatDau = DateTime.Now;

        public DateTime NgayBatDau
        {
            get { return _NgayBatDau; }
            set
            {
                _NgayBatDau = value;
                OnPropertyChanged();
            }
        }

        private DateTime _NgayKetThuc = DateTime.Now;

        public DateTime NgayKetThuc
        {
            get { return _NgayKetThuc; }
            set
            {
                _NgayKetThuc = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand XuatBaoCao { get; set; }

        public BaoCaoTongKetViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

            });

            XuatBaoCao = new RelayCommand<MainWindow>((p) => { return true; }, (p) =>
            {
                XuatBaoCaoFunction();
            });
        }

        public void XuatBaoCaoFunction()
        {
            if (NgayBatDau > NgayKetThuc)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Ngày bắt đầu trước Ngày Kết thúc", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            XuatBaoCaoTongKet baoCao = new XuatBaoCaoTongKet();
            baoCao.startDate = NgayBatDau;
            baoCao.endDate = NgayKetThuc;
            baoCao.maPhongBan = MaPhongBan;
            baoCao.PhongBan = PhongBan;
            baoCao.personPD = User;
            baoCao.ShowDialog();
        }
    }
}
