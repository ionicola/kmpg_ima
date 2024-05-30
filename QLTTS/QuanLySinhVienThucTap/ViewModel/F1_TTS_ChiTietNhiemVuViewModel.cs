using QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class F1_TTS_ChiTietNhiemVuViewModel : BaseViewModel
    {
        private string _TenDA;
        public string TenDA
        {
            get { return _TenDA; }
            set
            {
                _TenDA = value;
                OnPropertyChanged();
            }
        }

        private int _MaNhiemVuDA;

        public int MaNhiemVuDA
        {
            get { return _MaNhiemVuDA; }
            set
            {
                _MaNhiemVuDA = value;
                OnPropertyChanged();
            }
        }

        private string _TenNhiemVuDA;

        public string TenNhiemVuDA
        {
            get { return _TenNhiemVuDA; }
            set
            {
                _TenNhiemVuDA = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _NgayBatDau;

        public DateTime? NgayBatDau
        {
            get { return _NgayBatDau; }
            set
            {
                _NgayBatDau = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _Deadline;

        public DateTime? Deadline
        {
            get { return _Deadline; }
            set
            {
                _Deadline = value;
                OnPropertyChanged();
            }
        }

        private string _Status;

        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        private string _NhanXet;

        public string NhanXet
        {
            get { return _NhanXet; }
            set
            {
                _NhanXet = value;
                OnPropertyChanged();
            }
        }

        private int? _Diem;

        public int? Diem
        {
            get { return _Diem; }
            set
            {
                _Diem = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadWindowCommand { get; set; }

        public ICommand ExitCommand { get; set; }
        public F1_TTS_ChiTietNhiemVuViewModel()
        {
            LoadWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadChiTietNhiemVu();
            });

            ExitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                if (currentWindow != null)
                { 
                    currentWindow.Close();
                }
            });
        }

        public void LoadChiTietNhiemVu()
        {
            var hoanThanhNhiemVu = DataProvider.Ins.DB.tblNhiemVuDAs.SingleOrDefault(p => p.MaNhiemVuDA == MaNhiemVuDA);
            var ChiTietNhiemVu = DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.SingleOrDefault(p => p.MaNhiemVuDA == MaNhiemVuDA);
            TenDA = DataProvider.Ins.DB.tblDuAns.Where(p => p.MaDA == hoanThanhNhiemVu.MaDA).Select(p => p.TenDA).SingleOrDefault();
            TenNhiemVuDA = hoanThanhNhiemVu.NhiemVu;
            NgayBatDau = hoanThanhNhiemVu.NgayBatDau;
            Deadline = hoanThanhNhiemVu.Deadline;
            Status = hoanThanhNhiemVu.status;
            NhanXet = ChiTietNhiemVu.NhanXet;
            Diem = ChiTietNhiemVu.Diem;
        }
    }
}
