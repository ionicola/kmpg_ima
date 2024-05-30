using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.TTS_Page.TTS_QuanLyYeuCauPage;
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
    public class TTS_QuanLyYeuCauViewModel : BaseViewModel
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
        public ICommand LoadYeuCauCommand { get; set; }
        public ICommand CancelYeuCauCommand { get; set; }
        
        private TTS_QuanLyYeuCau _selectedYeuCau;

        public TTS_QuanLyYeuCau SelectedYeuCau
        {
            get { return _selectedYeuCau; }
            set
            {
                _selectedYeuCau = value;
                OnPropertyChanged(nameof(SelectedYeuCau));
            }
        }

        private ObservableCollection<TTS_QuanLyYeuCau> _ListYeuCau;
        public ObservableCollection<TTS_QuanLyYeuCau> ListYeuCau
        {
            get { return _ListYeuCau; }
            set
            {
                _ListYeuCau = value;
                OnPropertyChanged(nameof(ListYeuCau));
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

        public ICommand ThemYeuCauCommand { get; set; }
        public TTS_QuanLyYeuCauViewModel()
        {
            LoadYeuCauCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {
                LoadYeuCau();

            });

            CancelYeuCauCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                HuyBoYeuCau();
            });

            ThemYeuCauCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemYeuCau();
                LoadYeuCau();
            });
        }
        public void LoadYeuCau()
        {
            ListYeuCau = new ObservableCollection<TTS_QuanLyYeuCau>();
            var YeuCauList = DataProvider.Ins.DB.tblYeuCaus.Where(p => p.MaTTS == UserId).OrderByDescending(p => p.NgayHieuLuc).ToList();
            int i = 1;
            foreach (var item in YeuCauList)
            {
                TTS_QuanLyYeuCau cacyeucau = new TTS_QuanLyYeuCau();

                cacyeucau.MaYeuCau = item.MaYeuCau;
                cacyeucau.TenTTS = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == item.MaTTS).Select(p => p.TenTTS).SingleOrDefault(); ;
                cacyeucau.NgayTao = item.NgayNop;
                cacyeucau.NgayHieuLuc = item.NgayHieuLuc;
                cacyeucau.YeuCau = item.NoiDung;
                cacyeucau.Status = item.Status;
                cacyeucau.STT = i;
                ListYeuCau.Add(cacyeucau);
                i++;
            }
        }

        public void ThemYeuCau()
        {
            string TenTTS = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == UserId).Select(x => x.TenTTS).FirstOrDefault();
            ThemYeuCau themYeuCau = new ThemYeuCau(TenTTS, UserId);
            themYeuCau.ShowDialog();
        }


        public void HuyBoYeuCau()
        {
            if (SelectedYeuCau == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn yêu cầu cần hủy bỏ.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var maYeuCau = SelectedYeuCau.MaYeuCau;
            var yeuCauCanHuy = DataProvider.Ins.DB.tblYeuCaus.SingleOrDefault(p => p.MaYeuCau == maYeuCau);

            if (yeuCauCanHuy.Status == "pending")
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ yêu cầu này?", "Xác nhận hủy bỏ", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (yeuCauCanHuy != null)
                    {
                        DataProvider.Ins.DB.tblYeuCaus.Remove(yeuCauCanHuy);
                        DataProvider.Ins.DB.SaveChanges();
                    }
                    MessageBox.Show("Hủy bỏ yêu cầu thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadYeuCau();
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra! Yêu cầu đã được kiểm duyệt, không thể hủy bỏ.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
