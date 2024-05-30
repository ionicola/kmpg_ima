using QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class YeuCauDaPheDuyetViewModel : BaseViewModel
    {
        public ICommand LoadedCommand { get; set;  }
        private ObservableCollection<CacYeuCauDaPheDuyet> _ListYeuCauDaPheDuyet;
        public ObservableCollection<CacYeuCauDaPheDuyet> ListYeuCauDaPheDuyet
        {
            get { return _ListYeuCauDaPheDuyet; }
            set
            {
                _ListYeuCauDaPheDuyet = value;
                OnPropertyChanged(nameof(ListYeuCauDaPheDuyet));
            }
        }
        private string _userDepart;
        public string UserDepart
        {
            get { return _userDepart; }
            set
            {
                _userDepart = value;
                OnPropertyChanged(nameof(UserDepart));
            }
        }
        private CacYeuCauDaPheDuyet _SelectedYeuCauDaDuyet;
        public CacYeuCauDaPheDuyet SelectedYeuCauDaDuyet
        {
            get { return _SelectedYeuCauDaDuyet; }
            set
            {
                _SelectedYeuCauDaDuyet = value;
                OnPropertyChanged(nameof(SelectedYeuCauDaDuyet));
            }
        }
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

        public ICommand CancelPheDuyetCommand { get; set; }
        public YeuCauDaPheDuyetViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadYeuCauDaDuyet(UserDepart);

            });
        }
        public void LoadYeuCauDaDuyet(string UserDepart)
        {
            ListYeuCauDaPheDuyet = new ObservableCollection<CacYeuCauDaPheDuyet>();
            // Lấy danh sách MaTTS từ tblTTS
            var maTTSList = DataProvider.Ins.DB.tblTTS
            .Where(p => p.MaPhongBan == UserDepart)
            .Select(p => p.MaTTS)
            .ToList();

            var yeuCauChoPheDuyetList = DataProvider.Ins.DB.tblYeuCaus
            .Where(yc => yc.Status != "pending" &&
                 DataProvider.Ins.DB.tblNguoiPheDuyets
                    .Any(npd => npd.MaYeuCau == yc.MaYeuCau && npd.MaQL == UserId)).OrderByDescending(yc => yc.MaYeuCau).ToList();

            int i = 1;
            foreach (var item in yeuCauChoPheDuyetList)
            {
                CacYeuCauDaPheDuyet cacyeucau = new CacYeuCauDaPheDuyet();

                cacyeucau.STT = i;
                cacyeucau.MaTTS = item.MaTTS;
                cacyeucau.NgayTao = item.NgayNop;
                cacyeucau.TenTTS = DataProvider.Ins.DB.tblTTS
                                        .Where(tts => tts.MaTTS == item.MaTTS)
                                        .Select(tts => tts.TenTTS)
                                        .FirstOrDefault();
                cacyeucau.NgayHieuLuc = item.NgayHieuLuc;
                cacyeucau.MaYeuCau = item.MaYeuCau;
                cacyeucau.YeuCau = item.NoiDung;
                cacyeucau.Status = item.Status;
                ListYeuCauDaPheDuyet.Add(cacyeucau);
                i++;
            }
        }

    }
}
