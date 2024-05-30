using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.NS_Page.NS_BaoCaoChamCongPage;
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
    public class NS_BaoCaoChamCongViewModel : BaseViewModel
    {
        private string _User;

        public string User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged();
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
        private string _MaPhongBan;

        public string MaPhongBan
        {
            get { return _MaPhongBan; }
            set
            {
                _MaPhongBan = value;
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

        private ObservableCollection<tblPhongBan> _phongBanList;
        public ObservableCollection<tblPhongBan> PhongBanList
        {
            get { return _phongBanList; }
            set
            {
                _phongBanList = value;
                OnPropertyChanged(nameof(PhongBanList));
            }
        }

        private ObservableCollection<tblTT> _TTSList;
        public ObservableCollection<tblTT> TTSList
        {
            get { return _TTSList; }
            set
            {
                _TTSList = value;
                OnPropertyChanged(nameof(TTSList));
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
                LoadTTS();
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

        public bool isLoadPage = false;
        public ICommand LoadedCommand { get; set; }
        public ICommand XuatBaoCaoCommand { get; set; }
        public Action<object, EventArgs> NavigationRequested { get; internal set; }

        public NS_BaoCaoChamCongViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadPage();
            });

            XuatBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XuatBaoCao();
                LoadPage();
            });
        }

        public void LoadPage()
        {
            PhongBanList = new ObservableCollection<tblPhongBan>();
            var PhongBanlist = DataProvider.Ins.DB.tblPhongBans.ToList();
            foreach (var item in PhongBanlist)
            {
                tblPhongBan newPhongBanList = new tblPhongBan();
                newPhongBanList.MaPhongBan = item.MaPhongBan;
                newPhongBanList.TenPhongBan = item.TenPhongBan;
                PhongBanList.Add(newPhongBanList);
            }
        }

        public void LoadTTS()
        {
            if (SelectedPhongBan == null)
            {
                SelectedTTS = null;
                return;
            }
            TTSList = new ObservableCollection<tblTT>();
            var TTSlist = DataProvider.Ins.DB.tblTTS.Where(x => x.MaPhongBan == SelectedPhongBan.MaPhongBan).ToList();
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
            if (SelectedPhongBan == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Phòng Ban cần xuất báo cáo!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SelectedTTS == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Thực tập sinh cần xuất báo cáo!", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            XuatBaoCaoChamCong baoCaoChamCong = new XuatBaoCaoChamCong();
            baoCaoChamCong.startDate = NgayBatDau;
            baoCaoChamCong.endDate = NgayKetThuc;
            baoCaoChamCong.maPhongBan = SelectedPhongBan.MaPhongBan;
            baoCaoChamCong.PhongBan = SelectedPhongBan.TenPhongBan;
            baoCaoChamCong.personPD = User;
            baoCaoChamCong.MaTTSreport = SelectedTTS.MaTTS;
            baoCaoChamCong.nameTTS = SelectedTTS.TenTTS;

            baoCaoChamCong.ShowDialog();
        }
    }
}
