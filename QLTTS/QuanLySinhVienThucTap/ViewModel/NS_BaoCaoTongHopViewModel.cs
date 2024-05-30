using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.NS_Page;
using QuanLySinhVienThucTap.NS_Page.NS_BaoCaoTongHopPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class NS_BaoCaoTongHopViewModel : BaseViewModel
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
        public ICommand NS_XuatBaoCaoCommand { get; set; }
        public Action<object, EventArgs> NavigationRequested { get; internal set; }

        public NS_BaoCaoTongHopViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadPage();
            });

            NS_XuatBaoCaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
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
            if(SelectedPhongBan == null)
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
                MessageBox.Show("Vui lòng lựa chọn phòng ban cần xuất báo cáo.");
                return;
            }
            XuatBaoCaoTongHop baoCaoTongHop = new XuatBaoCaoTongHop();
            baoCaoTongHop.startDate = NgayBatDau;
            baoCaoTongHop.endDate = NgayKetThuc;
            baoCaoTongHop.maPhongBan = SelectedPhongBan.MaPhongBan;
            baoCaoTongHop.PhongBan = SelectedPhongBan.TenPhongBan;
            baoCaoTongHop.personPD = User;
            if (SelectedTTS != null)
            {
                baoCaoTongHop.MaTTSreport = SelectedTTS.MaTTS;
                baoCaoTongHop.nameTTS = SelectedTTS.TenTTS;
            }
            else
            {
                baoCaoTongHop.MaTTSreport = null;
                baoCaoTongHop.nameTTS = null;
            }
            baoCaoTongHop.ShowDialog();
        }
    }
}
