using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.TTS_Page;
using QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachKhoaHocPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class TTS_DanhSachKhoaHocViewModel : BaseViewModel
    {
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
        public ICommand LoadKhoaHocCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }

        private TTS_NhiemVuDaoTao _selectedKhoaHoc;

        public TTS_NhiemVuDaoTao SelectedKhoaHoc
        {
            get { return _selectedKhoaHoc; }
            set
            {
                _selectedKhoaHoc = value;
                OnPropertyChanged(nameof(SelectedKhoaHoc));
            }
        }

        private ObservableCollection<TTS_NhiemVuDaoTao> _ListKhoaHoc;
        public ObservableCollection<TTS_NhiemVuDaoTao> ListKhoaHoc
        {
            get { return _ListKhoaHoc; }
            set
            {
                _ListKhoaHoc = value;
                OnPropertyChanged(nameof(ListKhoaHoc));
            }
        }
        private ObservableCollection<TTS_NhiemVuCacDuAn> _ListNhiemVu;
        public ObservableCollection<TTS_NhiemVuCacDuAn> ListNhiemVu
        {
            get { return _ListNhiemVu; }
            set
            {
                _ListNhiemVu = value;
                OnPropertyChanged(nameof(ListNhiemVu));
            }
        }

        private string _tenKhoa;
        public string TenKhoa
        {
            get { return _tenKhoa; }
            set
            {
                _tenKhoa = value;
                OnPropertyChanged(nameof(TenKhoa));
            }
        }

        public TTS_DanhSachKhoaHocViewModel()
        {
            LoadKhoaHocCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {
                LoadNhiemVuObj();
            });

            CompleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                HoanThanhKhoaHoc();
                LoadNhiemVuObj();
            });

            DetailsCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChiTietKhoaHoc();
            });
        }
        public void LoadNhiemVuObj()
        {
            ListKhoaHoc = new ObservableCollection<TTS_NhiemVuDaoTao>();
            var KhoaHocList = DataProvider.Ins.DB.tblNhiemVuDaoTaos.Where(p => p.MaTTS == UserId && p.status == "in-progress").OrderByDescending(p=>p.Deadline).ToList();
            int i = 1;
            foreach (var item in KhoaHocList)
            {
                TTS_NhiemVuDaoTao cacnv = new TTS_NhiemVuDaoTao();

                cacnv.MaNhiemVuDaoTao = item.MaNhiemVuDaoTao;
                cacnv.MaKhoaDaoTao = item.MaKhoaDaoTao;
                cacnv.Deadline = item.Deadline;
                cacnv.TenKhoa = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(p => p.MaKhoaDaoTao == item.MaKhoaDaoTao).Select(p => p.TenKhoa).SingleOrDefault();
                cacnv.STT = i;
                cacnv.Status = item.status;
                ListKhoaHoc.Add(cacnv);
                i++;
            }
        }

        public void HoanThanhKhoaHoc()
        {
            if (SelectedKhoaHoc == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn nhiệm vụ đào tạo.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn đã hoàn thành khóa học này? Sau khi ấn đồng ý không thể hoàn tác.", "Xác nhận hoàn thành", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var maNhiemVuKhoaHoc = SelectedKhoaHoc.MaNhiemVuDaoTao;
                var hoanthanh = DataProvider.Ins.DB.tblNhiemVuDaoTaos.SingleOrDefault(p => p.MaNhiemVuDaoTao == maNhiemVuKhoaHoc);
                hoanthanh.status = "done";
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập nhật thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                return;
            }
        }

        public void ChiTietKhoaHoc()
        {
            if (SelectedKhoaHoc == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn nhiệm vụ đào tạo.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            TTS_ChiTietKhoaHoc chiTietKhoaHoc = new TTS_ChiTietKhoaHoc(SelectedKhoaHoc.MaKhoaDaoTao);
            chiTietKhoaHoc.Show();
        }

    }
}
