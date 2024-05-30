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
    public class SuaNhiemVuDuAnViewModel : BaseViewModel
    {
        private NhiemVuCacDuAn _selectedItemDA;

        public NhiemVuCacDuAn SelectedItemDA
        {
            get { return _selectedItemDA; }
            set
            {
                _selectedItemDA = value;
                OnPropertyChanged(nameof(SelectedItemDA));
            }
        }

        private string _NhiemVuDA;

        public string NhiemVuDA
        {
            get { return _NhiemVuDA; }
            set
            {
                _NhiemVuDA = value;
                OnPropertyChanged(nameof(NhiemVuDA));
            }
        }
        private DateTime? _ngayBatDau;
        private DateTime? _deadline;
        public DateTime? NgayBatDau
        {
            get { return _ngayBatDau; }
            set
            {
                _ngayBatDau = value;
                OnPropertyChanged(nameof(NgayBatDau));
            }
        }

        public DateTime? Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        private string _TenTTS;
        public string TenTTS
        {
            get { return _TenTTS; }
            set
            {
                _TenTTS = value;
                OnPropertyChanged(nameof(TenTTS));
            }
        }

        private string _MaNhiemVuDA;
        public string MaNhiemVuDA
        {
            get { return _MaNhiemVuDA; }
            set
            {
                _MaNhiemVuDA = value;
                OnPropertyChanged(nameof(MaNhiemVuDA));
            }
        }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand SuaNhiemVuDACommand { get; set; }
        public SuaNhiemVuDuAnViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadWindow();
            });

            SuaNhiemVuDACommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SuaNhiemVu();
            });
        }
        public void LoadWindow()
        {
            NgayBatDau = SelectedItemDA.NgayBatDau;
            Deadline = SelectedItemDA.Deadline;
            TenTTS = SelectedItemDA.TenTTS;
            MaNhiemVuDA = SelectedItemDA.MaNhiemVuDA;
            NhiemVuDA = SelectedItemDA.NhiemVu;
            
        }
        public void SuaNhiemVu()
        {
            int updateMaNV = int.Parse(SelectedItemDA.MaNhiemVuDA);
            var recordToUpdate = DataProvider.Ins.DB.tblNhiemVuDAs.FirstOrDefault(p => p.MaNhiemVuDA == updateMaNV);

            if (recordToUpdate != null)
            {
                if (NgayBatDau < Deadline)
                {
                    if (recordToUpdate.status != "expired")
                    {
                        recordToUpdate.NhiemVu = NhiemVuDA;
                        recordToUpdate.Deadline = Deadline;
                        recordToUpdate.NgayBatDau = NgayBatDau;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        recordToUpdate.NhiemVu = NhiemVuDA;
                        recordToUpdate.Deadline = Deadline;
                        recordToUpdate.NgayBatDau = NgayBatDau;
                        recordToUpdate.status = "in-progress";
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Ngày bắt đầu phải trước Deadline.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            } else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
