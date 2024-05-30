using QuanLySinhVienThucTap.Model;
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
    public class F1_TTS_ThemYeuCauViewModel : BaseViewModel
    {
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

        private string _MaTTS;

        public string MaTTS
        {
            get { return _MaTTS; }
            set
            {
                _MaTTS = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ngayHieuLuc;
        private DateTime _thoiGianHieuLuc;


        public DateTime ThoiGianHieuLuc
        {
            get { return _thoiGianHieuLuc; }
            set
            {
                if (_thoiGianHieuLuc != value)
                {
                    _thoiGianHieuLuc = value;
                    OnPropertyChanged(nameof(ThoiGianHieuLuc));
                }
            }
        }
        // Other properties and methods...
        public DateTime NgayHieuLuc
        {
            get { return _ngayHieuLuc + ThoiGianHieuLuc.TimeOfDay; }
            set
            {
                _ngayHieuLuc = value.Date;
                _thoiGianHieuLuc = ThoiGianHieuLuc;
                OnPropertyChanged(nameof(NgayHieuLuc));
            }
        }

        private string _yeuCau;
        public string YeuCau
        {
            get { return _yeuCau; }
            set
            {
                if (_yeuCau != value)
                {
                    _yeuCau = value;
                    OnPropertyChanged(nameof(YeuCau));
                }
            }
        }

        public ICommand ThemYeuCau { get; set; }
        public F1_TTS_ThemYeuCauViewModel()
        {
            _ngayHieuLuc = DateTime.Today.Date;
            ThemYeuCau = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddYeuCau();
            });

        }

        public void AddYeuCau()
        {
            if (YeuCau == null || YeuCau.Trim().Length < 4)
            {
                MessageBox.Show("Vui lòng nhập lại yêu cầu. Độ dài yêu cầu tối thiểu là 5 ký tự.");
                return;
            }

            tblYeuCau newYeuCau = new tblYeuCau
            {
                MaTTS = MaTTS,
                NoiDung = YeuCau,
                NgayHieuLuc = NgayHieuLuc,
                NgayNop = DateTime.Now,
                Status = "pending"
            };

            // Hiển thị MessageBox với lựa chọn OK hoặc Cancel
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm yêu cầu không?", "Xác nhận", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblYeuCaus.Add(newYeuCau);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm yêu cầu thành công!");
            }
            else
            {
                return;
            }
        }
    }
}
