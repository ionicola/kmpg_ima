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
    public class NS_BaoCaoLuongViewModel : BaseViewModel
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

        private ObservableCollection<tblPhongBan> _PhongBanList;
        public ObservableCollection<tblPhongBan> PhongBanList
        {
            get { return _PhongBanList; }
            set
            {
                _PhongBanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BaoCaoLuongModel> _BaoCaoLuongList;
        public ObservableCollection<BaoCaoLuongModel> BaoCaoLuongList
        {
            get { return _BaoCaoLuongList; }
            set
            {
                _BaoCaoLuongList = value;
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

        private DateTime? _NgayKetThuc;

        public DateTime? NgayKetThuc
        {
            get { return _NgayKetThuc; }
            set
            {

                _NgayKetThuc = value;
                OnPropertyChanged();
            }
        }

        private tblPhongBan _SelectedPhongBan;

        public tblPhongBan SelectedPhongBan
        {
            get { return _SelectedPhongBan; }
            set
            {
                    _SelectedPhongBan = value;
                    OnPropertyChanged();
            }
        }

        public ICommand LoadedCommand { get; set; }

        public ICommand XuatBaoCaoCommand { get; set; }

        public ICommand CancelCommand { get; set; }
        public NS_BaoCaoLuongViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadPage();
            });

            XuatBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedPhongBan == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Phòng Ban cần xuất báo cáo!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (NgayBatDau == null || NgayKetThuc == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Ngày bắt đầu và Ngày kết thúc!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (NgayBatDau >= NgayKetThuc)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Ngày bắt đầu trước Ngày Kết thúc", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                NgayKetThuc = NgayKetThuc.Value.AddDays(1);
                XuatBaoCao();
            });

            CancelCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedPhongBan = null;
                NgayBatDau = null;
                NgayKetThuc = null;
            });
        }

        public void LoadPage()
        {
            PhongBanList = new ObservableCollection<tblPhongBan>();
            var PhongBanlist = DataProvider.Ins.DB.tblPhongBans.ToList();
            foreach (var item in PhongBanlist)
            {
                tblPhongBan newPhongBan = new tblPhongBan();
                newPhongBan.MaPhongBan = item.MaPhongBan;
                newPhongBan.TenPhongBan = item.TenPhongBan;
                PhongBanList.Add(newPhongBan);
            }
        }
        public void XuatBaoCao()
        {
            BaoCaoLuongList = new ObservableCollection<BaoCaoLuongModel>();

            var nhanSuList = DataProvider.Ins.DB.tblTTS.Where(x => x.MaPhongBan == SelectedPhongBan.MaPhongBan).ToList();

            int i = 1;
            foreach (var item in nhanSuList)
            {
                BaoCaoLuongModel newBaoCao = new BaoCaoLuongModel();
                newBaoCao.STT = i;
                newBaoCao.MaTTS = item.MaTTS;
                newBaoCao.TenTTS = item.TenTTS;
                newBaoCao.SoNgayChamCong = DataProvider.Ins.DB.tblChamCongs.Where(x => x.MaTTS == item.MaTTS && x.NgayChamCong >= NgayBatDau && x.NgayChamCong <= NgayKetThuc).Count();
                newBaoCao.Luong = (4000000 / 22 * newBaoCao.SoNgayChamCong).ToString("N0");
                BaoCaoLuongList.Add(newBaoCao);
                i++;
            }
        }
    }
}
