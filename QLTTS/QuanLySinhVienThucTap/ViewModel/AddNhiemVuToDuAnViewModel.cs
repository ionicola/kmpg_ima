using MaterialDesignThemes.Wpf;
using QuanLySinhVienThucTap.Model;
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
    public class AddNhiemVuToDuAnViewModel : BaseViewModel
    {
        private string _maDuAn;

        public string maDuAn
        {
            get { return _maDuAn; }
            set
            {
                _maDuAn = value;
                OnPropertyChanged();
            }
        }

        private string _TenTTS;

        public string TenTTS
        {
            get { return _TenTTS; }
            set
            {
                _TenTTS = value;
                OnPropertyChanged();
            }
        }

        private string _MaTTS;

        public string MaTTS
        {
            get { return _MaTTS; }
            set
            {
                _MaTTS = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ngayBatDau;
        private DateTime _thoiGianBatDau;
        private DateTime _deadline;
        private DateTime _thoiGianDeadline;

        public DateTime ThoiGianDeadline
        {
            get { return _thoiGianDeadline; }
            set
            {
                if (_thoiGianDeadline != value)
                {
                    _thoiGianDeadline = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ThoiGianBatDau
        {
            get { return _thoiGianBatDau; }
            set
            {
                if (_thoiGianBatDau != value)
                {
                    _thoiGianBatDau = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime NgayBatDau
        {
            get { return _ngayBatDau + ThoiGianBatDau.TimeOfDay; }
            set
            {
                _ngayBatDau = value.Date;
                _thoiGianBatDau = ThoiGianBatDau;
                OnPropertyChanged();
            }
        }

        public DateTime Deadline
        {
            get { return _deadline + ThoiGianDeadline.TimeOfDay; }
            set
            {
                _deadline = value.Date;
                _thoiGianDeadline = ThoiGianDeadline;
                OnPropertyChanged();
            }
        }

        private string _nhiemVu;
        public string NhiemVu
        {
            get { return _nhiemVu; }
            set
            {
                if (_nhiemVu != value)
                {
                    _nhiemVu = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand AddTaskToDuAn { get; set; }
        public AddNhiemVuToDuAnViewModel()
        {
            _ngayBatDau = DateTime.Today.Date;
            _deadline = DateTime.Today.AddDays(1).Date;
            AddTaskToDuAn = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddTask();
            });

        }

        public void AddTask()
        {
            if (NhiemVu == null || NhiemVu.Trim().Length < 5)
            {
                MessageBox.Show("Có lỗi xảy ra! Nội dung nhiệm vụ quá ngắn, vui lòng nhập trên 5 ký tự.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            tblNhiemVuDA newnhiemvuduan = new tblNhiemVuDA
            {
                MaDA = maDuAn,
                MaTTS = MaTTS,
                NgayBatDau = NgayBatDau,
                Deadline = Deadline,
                NhiemVu = NhiemVu,
                status = "in-progress"

            };


            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhiệm vụ không?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblNhiemVuDAs.Add(newnhiemvuduan);
                DataProvider.Ins.DB.SaveChanges();
                tblNhanXetNhiemVuDA newNhanXetDA = new tblNhanXetNhiemVuDA
                {
                    MaNhiemVuDA = newnhiemvuduan.MaNhiemVuDA,
                    NhanXet = null,
                    Diem = null
                };
                DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.Add(newNhanXetDA);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm nhiệm vụ thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                OnPropertyChanged(nameof(QuanLyNhanSuViewModel.ListNhiemVuDuAn));
            }
            else
            {
                MessageBox.Show("Đã hủy!", "Hủy!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
