using QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class SuaNhiemVuDaoTaoViewModel : BaseViewModel
    {
        private CacNhiemVuDaoTao _selectedNhiemVuDaoTao;

        public CacNhiemVuDaoTao SelectedNhiemVuDaoTao
        {
            get { return _selectedNhiemVuDaoTao; }
            set
            {
                _selectedNhiemVuDaoTao = value;
                OnPropertyChanged();
            }
        }

        private string _KhoaDaoTao;
        public string KhoaDaoTao
        {
            get { return _KhoaDaoTao; }
            set
            {
                _KhoaDaoTao = value;
                OnPropertyChanged(nameof(KhoaDaoTao));
            }
        }

        private string _MaTTS;
        public string MaTTS
        {
            get { return _MaTTS; }
            set
            {
                _MaTTS = value;
                OnPropertyChanged(nameof(MaTTS));
            }
        }

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

        private DateTime? _ngayBatDau = DateTime.Now;
        private DateTime? _deadline = DateTime.Now;
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

        public ICommand LoadWindowCommand { get; set; }

        public ICommand CapNhatCommand{ get; set; }

        public SuaNhiemVuDaoTaoViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadWindow();
            });

            CapNhatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                    CapNhatNhiemVuDaoTao();
            });

        }

        public void LoadWindow()
        {
            MaTTS = SelectedNhiemVuDaoTao.MaTTS;
            TenTTS = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS.ToString() == SelectedNhiemVuDaoTao.MaTTS).Select(x => x.TenTTS).SingleOrDefault();
            KhoaDaoTao = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(x => x.MaKhoaDaoTao == MaKhoaDaoTao).Select(x => x.TenKhoa).SingleOrDefault();
            NgayBatDau = DataProvider.Ins.DB.tblNhiemVuDaoTaos.Where(x => x.MaNhiemVuDaoTao == SelectedNhiemVuDaoTao.MaNhiemVuDaoTao).Select(x => x.NgayBatDau).SingleOrDefault();
            Deadline = SelectedNhiemVuDaoTao.Deadline;
        }

        public void CapNhatNhiemVuDaoTao()
        {

            var recordToUpdate = DataProvider.Ins.DB.tblNhiemVuDaoTaos.FirstOrDefault(p => p.MaNhiemVuDaoTao == SelectedNhiemVuDaoTao.MaNhiemVuDaoTao);

            if (recordToUpdate != null)
            {
                if (NgayBatDau < Deadline)
                {
                    if (recordToUpdate.status != "expired")
                    {
                        recordToUpdate.Deadline = Deadline;
                        recordToUpdate.NgayBatDau = NgayBatDau;
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    else
                    {
                        recordToUpdate.Deadline = Deadline;
                        recordToUpdate.NgayBatDau = NgayBatDau;
                        recordToUpdate.status = "in-progress";
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Cập nhật thông tin Nhiệm vụ đào tạo thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Ngày bắt đầu phải trước Deadline.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
