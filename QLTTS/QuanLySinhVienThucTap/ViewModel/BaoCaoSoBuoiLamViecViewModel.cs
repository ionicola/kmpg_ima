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
    public class BaoCaoSoBuoiLamViecViewModel : BaseViewModel
    {
        private string _MaPhongBan;

        public string MaPhongBan
        {
            get { return _MaPhongBan; }
            set
            {
                if (_MaPhongBan != value)
                {
                    _MaPhongBan = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<tblTT> _TTSList;
        public ObservableCollection<tblTT> TTSList
        {
            get { return _TTSList; }
            set
            {
                _TTSList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BaoCaoSoBuoiLamViecModel> _ChamCongList;
        public ObservableCollection<BaoCaoSoBuoiLamViecModel> ChamCongList
        {
            get { return _ChamCongList; }
            set
            {
                _ChamCongList = value;
                OnPropertyChanged();
            }
        }

        private tblTT _SelectedTTS;

        public tblTT SelectedTTS
        {
            get { return _SelectedTTS; }
            set
            {
                _SelectedTTS = value;
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

        private int _TongSoNgay;

        public int TongSoNgay
        {
            get { return _TongSoNgay; }
            set
            {
                _TongSoNgay = value;
                OnPropertyChanged();
            }
        }

        private int _TongSoNgayChamCong;

        public int TongSoNgayChamCong
        {
            get { return _TongSoNgayChamCong; }
            set
            {
                _TongSoNgayChamCong = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand XuatBaoCaoCommand { get; set; }

        public BaoCaoSoBuoiLamViecViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadPage();
            });

            XuatBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedTTS == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Thực tập sinh cần xuất báo cáo!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (NgayBatDau == null || NgayKetThuc == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Ngày bắt đầu và Ngày kết thúc!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (NgayBatDau > NgayKetThuc)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Ngày bắt đầu trước Ngày Kết thúc", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                XuatBaoCao();
            });
        }

        public void LoadPage()
        {
            TTSList = new ObservableCollection<tblTT>();
            var TTSlist = DataProvider.Ins.DB.tblTTS.Where(x => x.MaPhongBan == MaPhongBan).ToList();
            foreach (var item in TTSlist)
            {
                tblTT newTTSlist = new tblTT();
                newTTSlist.MaTTS = item.MaTTS;
                newTTSlist.TenTTS = item.TenTTS;
                TTSList.Add(newTTSlist);
            }
        }

        public void XuatBaoCao()
        {
            ChamCongList = new ObservableCollection<BaoCaoSoBuoiLamViecModel>();
            var chamCongList = DataProvider.Ins.DB.tblChamCongs.Where(x => x.MaTTS == SelectedTTS.MaTTS && x.NgayChamCong >= NgayBatDau && x.NgayChamCong <= NgayKetThuc).OrderBy(x => x.NgayChamCong).ToList();
            var count = (int)(NgayKetThuc - NgayBatDau).TotalDays;
            var nvDone = chamCongList.Count();
            string tenTTS = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == SelectedTTS.MaTTS).Select(x => x.TenTTS).SingleOrDefault();
            if (count == 0)
            {
                MessageBox.Show("Thực tập sinh không chấm công trong khoảng thời gian này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int i = 1;
            foreach (var item in chamCongList)
            {
                BaoCaoSoBuoiLamViecModel newChamCong = new BaoCaoSoBuoiLamViecModel();
                newChamCong.STT = i;
                newChamCong.MaTTS = item.MaTTS;
                newChamCong.TenTTS = tenTTS;
                newChamCong.NgayChamCong = item.NgayChamCong;
                ChamCongList.Add(newChamCong);
                i++;
            }

            TongSoNgay = count;
            TongSoNgayChamCong = nvDone;

        }
    }
}
