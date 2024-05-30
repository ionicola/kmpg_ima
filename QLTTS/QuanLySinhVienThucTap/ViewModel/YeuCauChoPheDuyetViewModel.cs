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

    public class YeuCauChoPheDuyetViewModel : BaseViewModel
    {
        public ICommand LoadedCommand { get; set; }

        private ObservableCollection<CacYeuCauChoPheDuyet> _ListYeuCauChoPheDuyet;
        public ObservableCollection<CacYeuCauChoPheDuyet> ListYeuCauChoPheDuyet
        {
            get { return _ListYeuCauChoPheDuyet; }
            set
            {
                _ListYeuCauChoPheDuyet = value;
                OnPropertyChanged(nameof(ListYeuCauChoPheDuyet));
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

        private CacYeuCauChoPheDuyet _SelectedYeuCauChoDuyet;
        public CacYeuCauChoPheDuyet SelectedYeuCauChoDuyet
        {
            get { return _SelectedYeuCauChoDuyet; }
            set
            {
                _SelectedYeuCauChoDuyet = value;
                OnPropertyChanged(nameof(SelectedYeuCauChoDuyet));
            }
        }

        public ICommand PheDuyetCommand { get; set; }
        public ICommand TuChoiCommand { get; set; }
        public YeuCauChoPheDuyetViewModel() 
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                LoadYeuCauChoDuyet(UserDepart);

            });

            PheDuyetCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PheDuyetYeuCau();
                LoadYeuCauChoDuyet(UserDepart);
            });

            TuChoiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TuChoiYeuCau();
                LoadYeuCauChoDuyet(UserDepart);
            });
        }

        public int count = 0;
        public void LoadYeuCauChoDuyet(string UserDepart)
        {
            ListYeuCauChoPheDuyet = new ObservableCollection<CacYeuCauChoPheDuyet>();
            var maTTSList = DataProvider.Ins.DB.tblTTS
    .Where(p => p.MaPhongBan == UserDepart)
    .Select(p => p.MaTTS)
    .ToList();

            var yeuCauChoPheDuyetList = DataProvider.Ins.DB.tblYeuCaus
    .Where(yc => yc.Status == "pending" && maTTSList.Contains(yc.MaTTS)).OrderByDescending(yc => yc.MaYeuCau)
    .ToList();

            int i = 1;
            foreach (var item in yeuCauChoPheDuyetList)
            {
                CacYeuCauChoPheDuyet cacyeucau = new CacYeuCauChoPheDuyet();

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
                ListYeuCauChoPheDuyet.Add(cacyeucau);
                i++;
            }
            count = i;
        }

        public void PheDuyetYeuCau()
        {
            if (count == 1)
            {
                MessageBox.Show("Đã phê duyệt hết toàn bộ yêu cầu.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (SelectedYeuCauChoDuyet == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn yêu cầu phê duyệt.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn phê duyệt yêu cầu này? Sau khi thực hiện không thể hoàn tác.", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var maYeuCau = SelectedYeuCauChoDuyet.MaYeuCau;
                var yeuCauDuyet = DataProvider.Ins.DB.tblYeuCaus.SingleOrDefault(p => p.MaYeuCau == maYeuCau);
                yeuCauDuyet.Status = "pass";
                var nguoiDuyet = new tblNguoiPheDuyet
                {
                    MaYeuCau = maYeuCau,
                    MaQL = UserId,
                };
                DataProvider.Ins.DB.tblNguoiPheDuyets.Add(nguoiDuyet);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Phê duyệt thành công.");
            }
            else
            {
                return;
            }
        }

        public void TuChoiYeuCau()
        {
            if (count == 1)
            {
                MessageBox.Show("Đã phê duyệt hết toàn bộ yêu cầu.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            else if (SelectedYeuCauChoDuyet == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn yêu cầu phê duyệt.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn từ chối yêu cầu này? Sau khi thực hiện không thể hoàn tác.", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var maYeuCau = SelectedYeuCauChoDuyet.MaYeuCau;
                var yeuCauDuyet = DataProvider.Ins.DB.tblYeuCaus.SingleOrDefault(p => p.MaYeuCau == maYeuCau);

                yeuCauDuyet.Status = "reject";

                var nguoiDuyet = new tblNguoiPheDuyet
                {
                    MaYeuCau = maYeuCau,
                    MaQL = UserId,
                };
                DataProvider.Ins.DB.tblNguoiPheDuyets.Add(nguoiDuyet);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Phê duyệt thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                return;
            }
        }
    }
}
