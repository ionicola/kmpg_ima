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
    public class BaoCaoKetQuaDaoTaoViewModel : BaseViewModel
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

        private ObservableCollection<BaoCaoNhiemVuDaoTaoModel> _NhiemVuDaoTaoList;
        public ObservableCollection<BaoCaoNhiemVuDaoTaoModel> NhiemVuDaoTaoList
        {
            get { return _NhiemVuDaoTaoList; }
            set
            {
                _NhiemVuDaoTaoList = value;
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

        private int _TongSo;

        public int TongSo
        {
            get { return _TongSo; }
            set
            {
                _TongSo = value;
                OnPropertyChanged();
            }
        }

        private double _TiLeHoanThanh;

        public double TiLeHoanThanh
        {
            get { return _TiLeHoanThanh; }
            set
            {
                _TiLeHoanThanh = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadedCommand { get; set; }
        public ICommand XuatBaoCaoCommand { get; set; }

        public BaoCaoKetQuaDaoTaoViewModel()
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
            NhiemVuDaoTaoList = new ObservableCollection<BaoCaoNhiemVuDaoTaoModel>();
            var nhiemVuDaoTao = DataProvider.Ins.DB.tblNhiemVuDaoTaos.Where(x => x.MaTTS == SelectedTTS.MaTTS && x.NgayBatDau >= NgayBatDau && x.NgayBatDau <= NgayKetThuc).OrderBy(x => x.NgayBatDau).ToList();
            var count = nhiemVuDaoTao.Count();
            var nvDone = nhiemVuDaoTao.Count(x => x.status == "done" || x.status == "approved");
            if (count == 0)
            {
                MessageBox.Show("Thực tập sinh không có Nhiệm vụ đào tạo nào trong khoảng thời gian này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            int i = 1;
            foreach (var item in nhiemVuDaoTao)
            {
                BaoCaoNhiemVuDaoTaoModel newNhiemVu = new BaoCaoNhiemVuDaoTaoModel();
                newNhiemVu.STT = i;
                newNhiemVu.MaKhoaDaoTao = item.MaKhoaDaoTao;
                newNhiemVu.MaNhiemVuDaoTao = item.MaNhiemVuDaoTao.ToString();
                newNhiemVu.TenKhoa = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(x => x.MaKhoaDaoTao == item.MaKhoaDaoTao).Select(x => x.TenKhoa).SingleOrDefault();
                newNhiemVu.TenTTS = SelectedTTS.TenTTS;
                newNhiemVu.NgayBatDau = item.NgayBatDau;
                newNhiemVu.Deadline = item.Deadline;
                newNhiemVu.status = item.status;

                NhiemVuDaoTaoList.Add(newNhiemVu);
                i++;
            }

            TongSo = count;
            TiLeHoanThanh = Math.Round(((double)nvDone / count)*100, 2);

        }
    }
}
