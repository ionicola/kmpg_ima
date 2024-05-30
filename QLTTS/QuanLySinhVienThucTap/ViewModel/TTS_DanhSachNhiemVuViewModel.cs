using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.TTS_Page.TTS_DanhSachNhiemVuPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class TTS_DanhSachNhiemVuViewModel : BaseViewModel
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
        public ICommand LoadDuAnCommand { get; set; }

        private CacDuAn _selectedDuAn;

        public CacDuAn SelectedDuAn
        {
            get { return _selectedDuAn; }
            set
            {
                _selectedDuAn = value;
                OnPropertyChanged(nameof(SelectedDuAn));
            }
        }

        private TTS_NhiemVuCacDuAn _selectedNhiemVu;

        public TTS_NhiemVuCacDuAn SelectedNhiemVu
        {
            get { return _selectedNhiemVu; }
            set
            {
                _selectedNhiemVu = value;
                OnPropertyChanged(nameof(SelectedNhiemVu));
            }
        }

        private ObservableCollection<CacDuAn> _ListDuAn;
        public ObservableCollection<CacDuAn> ListDuAn
        {
            get { return _ListDuAn; }
            set
            {
                _ListDuAn = value;
                OnPropertyChanged(nameof(ListDuAn));
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

        private string _tenDuAn;

        public string TenDuAn
        {
            get { return _tenDuAn; }
            set
            {
                _tenDuAn = value;
                OnPropertyChanged(nameof(TenDuAn));
            }
        }

        public ICommand CompleteNhiemVuCommand { get; set; }
        public ICommand XemChiTietCommand { get; set; }
        public TTS_DanhSachNhiemVuViewModel()
        {
            LoadDuAnCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {

                if(SelectedDuAn != null)
                {
                    TenDuAn = SelectedDuAn.TenDA;
                    LoadDuAn(UserId, SelectedDuAn.MaDA);
                }
            });

            CompleteNhiemVuCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedDuAn == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                HoanThanhNhiemVu();
                LoadDuAn(UserId, SelectedDuAn.MaDA);
            });

            XemChiTietCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedDuAn == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                XemChiTiet();
            });
        }


        public void LoadDuAnObj()
        {
            ListDuAn = new ObservableCollection<CacDuAn>();
            var maDAList = DataProvider.Ins.DB.tblNhanSuDAs.Where(p => p.MaTTS == UserId).Select(p => p.MaDA).ToList();
            var nhiemVuList = DataProvider.Ins.DB.tblDuAns
                .Where(nv => maDAList.Contains(nv.MaDA))
                .ToList();
            foreach (var item in nhiemVuList)
            {
                CacDuAn cacduan = new CacDuAn();

                cacduan.MaDA = item.MaDA;
                cacduan.TenDA = item.TenDA;
                ListDuAn.Add(cacduan);
            }
        }

        public void LoadDuAn(string UserId, string MaDA)
        {
            ListNhiemVu = new ObservableCollection<TTS_NhiemVuCacDuAn>();
            var nhiemVuList = DataProvider.Ins.DB.tblNhiemVuDAs
            .Where(nv => nv.MaTTS == UserId && nv.MaDA == MaDA)
            .OrderByDescending(nv => nv.MaNhiemVuDA)
            .ToList();
            int i = 1;

            foreach (var item in nhiemVuList)
            {
                TTS_NhiemVuCacDuAn nvcda = new TTS_NhiemVuCacDuAn();

                nvcda.STT = i;
                nvcda.MaNhiemVuDA = item.MaNhiemVuDA;
                nvcda.NgayBatDau = item.NgayBatDau;
                nvcda.TenTTS = DataProvider.Ins.DB.tblTTS
                                        .Where(tts => tts.MaTTS == item.MaTTS)
                                        .Select(tts => tts.TenTTS)
                                        .FirstOrDefault();
                nvcda.Deadline = item.Deadline;
                nvcda.NhiemVu = item.NhiemVu;
                nvcda.Status = item.status;
                ListNhiemVu.Add(nvcda);
                i++;
            }
        }

        public void HoanThanhNhiemVu()
        {
          if (SelectedNhiemVu == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn nhiệm vụ đã hoàn thành.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

                var maNhiemVu = SelectedNhiemVu.MaNhiemVuDA;
                var hoanThanhNhiemVu = DataProvider.Ins.DB.tblNhiemVuDAs.SingleOrDefault(p => p.MaNhiemVuDA == maNhiemVu);
            if (hoanThanhNhiemVu != null && hoanThanhNhiemVu.status == "in-progress")
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật hoàn thành nhiệm vụ này? Sau khi thực hiện không thể hoàn tác.", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    hoanThanhNhiemVu.status = "done";
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật nhiệm vụ thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn nhiệm vụ đang thực hiện (in-progress).", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        public void XemChiTiet()
        {
            if (SelectedNhiemVu == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn nhiệm vụ cần học.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ChiTietNhiemVu newChiTietNhiemVu = new ChiTietNhiemVu(SelectedNhiemVu.MaNhiemVuDA);
            newChiTietNhiemVu.ShowDialog();
        }

    }
}
