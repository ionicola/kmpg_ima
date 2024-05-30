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
    public class ThemNhiemVuDaoTaoViewModel : BaseViewModel
    {
        private NhanSuCacDuAn _selectedTTS;

        public NhanSuCacDuAn SelectedTTS
        {
            get { return _selectedTTS; }
            set
            {
                _selectedTTS = value;
                OnPropertyChanged(nameof(SelectedTTS));
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

        private tblKhoaDaoTao _SelectedKhoaHoc;

        public tblKhoaDaoTao SelectedKhoaHoc
        {
            get { return _SelectedKhoaHoc; }
            set
            {
                _SelectedKhoaHoc = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<NhanSuCacDuAn> _TTSList;
        public ObservableCollection<NhanSuCacDuAn> TTSList
        {
            get { return _TTSList; }
            set
            {
                _TTSList = value;
                OnPropertyChanged(nameof(TTSList));
            }
        }

        private DateTime _ngayBatDau = DateTime.Now;
        private DateTime _deadline = DateTime.Now;
        public DateTime NgayBatDau
        {
            get { return _ngayBatDau; }
            set
            {
                _ngayBatDau = value;
                OnPropertyChanged(nameof(NgayBatDau));
            }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        private string _SelectedKhoaDaoTao { get; set; }
        public string SelectedKhoaDaoTao
        {
            get { return _SelectedKhoaDaoTao; }
            set
            {
                _SelectedKhoaDaoTao = value;
                OnPropertyChanged(nameof(SelectedKhoaDaoTao));
            }
        }
        public ICommand ThemNhiemVuDaoTaoCommand { get; set; }

        public ICommand LoadWindowCommand { get; set; }

        public ThemNhiemVuDaoTaoViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadWindow();
            });

            ThemNhiemVuDaoTaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemNhiemVu();
                SelectedTTS = null;
                SelectedKhoaHoc = null;
            });
        }

        public void LoadWindow()
        {
            TTSList = new ObservableCollection<NhanSuCacDuAn>();

            var ThucTapSinhList = DataProvider.Ins.DB.tblTTS.Where(p => p.MaPhongBan == PhongBan).ToList();
            foreach (var item in ThucTapSinhList)
            {
                NhanSuCacDuAn newItem = new NhanSuCacDuAn
                {
                    MaTTS = item.MaTTS,
                    TenTTS = item.TenTTS
                };
                TTSList.Add(newItem);
            }
            KhoaDaoTao = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(p => p.MaKhoaDaoTao == SelectedKhoaDaoTao).Select(p => p.TenKhoa).FirstOrDefault();
            
        }

        public void ThemNhiemVu()
        {
            if (SelectedTTS == null)
            {
                MessageBoxResult result = MessageBox.Show("Có lỗi xảy ra. Vui lòng chọn thực tập sinh.", "Cảnh báo",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NgayBatDau >= Deadline)
            {
                MessageBox.Show("Có lỗi xảy ra. Ngày Bắt đầu phải trước Deadline.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult msgresult = MessageBox.Show($"Bạn có chắc chắn muốn thêm nhiệm vụ đào tạo cho {SelectedTTS.TenTTS} không?", "Xác nhận", MessageBoxButton.OKCancel);

            if (msgresult == MessageBoxResult.OK)
            {
                tblNhiemVuDaoTao newNhiemVu = new tblNhiemVuDaoTao
                {
                    MaKhoaDaoTao = SelectedKhoaDaoTao,
                    MaTTS = SelectedTTS.MaTTS.ToString(),
                    NgayBatDau = NgayBatDau,
                    Deadline = Deadline,
                    status = "in-progress"
                };

                DataProvider.Ins.DB.tblNhiemVuDaoTaos.Add(newNhiemVu);
                DataProvider.Ins.DB.SaveChanges();

                MessageBox.Show($"Thêm Nhiệm vụ Đào tạo cho {SelectedTTS.TenTTS} thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            } else
            {
                return;
            }
        }
    }
}
