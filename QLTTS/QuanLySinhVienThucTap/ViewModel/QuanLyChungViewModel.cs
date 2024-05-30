using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages.QuanLyChungPage;
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
    public class QuanLyChungViewModel : BaseViewModel
    {
        public string UserDepart;
        private ObservableCollection<tblTT> _TTSList;
        public ObservableCollection<tblTT> TTSList
        {
            get { return _TTSList; }
            set
            {
                _TTSList = value;
                OnPropertyChanged(nameof(_TTSList));
            }
        }
        public string UserId;
        private ObservableCollection<tblTT> _DAList;
        public ObservableCollection<tblTT> DAList
        {
            get { return _DAList; }
            set
            {
                _DAList = value;
                OnPropertyChanged(nameof(_DAList));
            }
        }
        public ICommand LoadedCommand { get; set; }
        private ObservableCollection<NhanSuCacDuAn> _NhanSuList;
        public ObservableCollection<NhanSuCacDuAn> NhanSuList
        {
            get { return _NhanSuList; }
            set
            {
                _NhanSuList = value;
                OnPropertyChanged(nameof(NhanSuList));
            }
        }
        private ObservableCollection<CacDuAn> _DuAnList;
        public ObservableCollection<CacDuAn> DuAnList
        {
            get { return _DuAnList; }
            set
            {
                _DuAnList = value;
                OnPropertyChanged(nameof(DuAnList));
            }
        }

        private NhanSuCacDuAn _selectedItem;

        public NhanSuCacDuAn SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private NhanSuCacDuAn _selectedTTS;

        public NhanSuCacDuAn SelectedTTS
        {
            get { return _selectedTTS; }
            set
            {
                _selectedTTS = value;
                OnPropertyChanged(nameof(SelectedTTS));
            }
        }

        private CacDuAn _selectedDA;

        public CacDuAn SelectedDA
        {
            get { return _selectedDA; }
            set
            {
                _selectedDA = value;
                OnPropertyChanged(nameof(SelectedDA));
            }
        }
        public ICommand TaoMoiTTSCommand { get; set; }
        public ICommand SuaTTSCommand { get; set; }
        public ICommand XoaTTSCommand { get; set; }
        public ICommand TaoMoiDACommand { get; set; }
        public ICommand ThemDACommand { get; set; }
        public ICommand BoThemDACommand { get; set; }
        public ICommand XoaDACommand { get; set; }
        public QuanLyChungViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadNhanSu();
                LoadDuAn();
            });

            TaoMoiTTSCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TaoMoiTTS();
                LoadNhanSu();
            });

            SuaTTSCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SuaTTS();
                LoadNhanSu();
            });

            XoaTTSCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XoaTTS();
                LoadNhanSu();
            });

            TaoMoiDACommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TaoMoiDuAn();
                LoadDuAn();
            });
            ThemDACommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemDuAn();
                LoadDuAn();
            });
            BoThemDACommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BoThemDuAn();
                LoadDuAn();
            });
            XoaDACommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XoaDuAn();
                LoadDuAn();
            });
        }

        public void LoadNhanSu()
        {
            NhanSuList = new ObservableCollection<NhanSuCacDuAn>();
            var ttsList = DataProvider.Ins.DB.tblTTS.Where(p => p.MaPhongBan == UserDepart).ToList();
            var phongBan = DataProvider.Ins.DB.tblPhongBans.Where(p => p.MaPhongBan == UserDepart).Select(p => p.TenPhongBan).SingleOrDefault();
            foreach (var tts in ttsList)
            {
                NhanSuCacDuAn nhansuduan = new NhanSuCacDuAn();
                nhansuduan.MaTTS = tts.MaTTS;
                nhansuduan.TenTTS = tts.TenTTS;
                var chucVu = DataProvider.Ins.DB.tblChucVus.FirstOrDefault(cv => cv.MaChucVu == tts.MaChucVu);

                if (chucVu != null)
                {
                    nhansuduan.MaChucVu = chucVu.TenChucVu;
                }
                else
                {
                    nhansuduan.MaChucVu = "Chưa xác định";
                }

                nhansuduan.MaPhongBan = phongBan ;
                NhanSuList.Add(nhansuduan);
            }
        }
        public void LoadDuAn()
        {
            DuAnList = new ObservableCollection<CacDuAn>();

            var maDuAnList = DataProvider.Ins.DB.tblTrucThuocs
                .Where(tt => tt.MaPhongBan == UserDepart)
                .Select(tt => tt.MaDA)
                .ToList();

            var duAnList = DataProvider.Ins.DB.tblDuAns
                .Where(da => maDuAnList.Contains(da.MaDA))
                .ToList();

            foreach (var da in duAnList)
            {
                CacDuAn cacduan = new CacDuAn();
                cacduan.MaDA = da.MaDA;
                cacduan.TenDA = da.TenDA;
                DuAnList.Add(cacduan);
            }
        }

        public void TaoMoiTTS()
        {
            TaoMoiTTS newTTS = new TaoMoiTTS(UserDepart);
            newTTS.ShowDialog();
        }

        public void SuaTTS()
        {
            if (SelectedTTS == null)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng chọn Thực tập sinh.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SuaTTS newSuaTTS = new SuaTTS(SelectedTTS);
            newSuaTTS.ShowDialog();
        }
        public void XoaTTS()
        {
            if (SelectedTTS == null)
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng chọn Thực tập sinh.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var maTTSDelete = SelectedTTS.MaTTS;

            var TTSCanXoa = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == maTTSDelete);

            MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa Thực tập sinh này? Điều này đồng nghĩa với việc toàn bộ dữ liệu về Thực tập sinh sẽ được xóa hoàn toàn khỏi hệ thống.", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.tblTTS.RemoveRange(TTSCanXoa);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa Thực tập sinh thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                else
                {
                    return;
                }
            }

        public void TaoMoiDuAn()
        {
            TaoMoiDuAn newDuAn = new TaoMoiDuAn(UserId);
            newDuAn.ShowDialog();
        }

        public void ThemDuAn()
        {
            ThemDuAn newDuAnHere = new ThemDuAn(UserDepart);
            newDuAnHere.ShowDialog();
        }

        public void BoThemDuAn()
        {
            if (SelectedDA == null)
            {
                MessageBox.Show("Vui lòng chọn Dự án!");
                return;
            }

            var maDADeleteHere = SelectedDA.MaDA;

            var listTTSHere = DataProvider.Ins.DB.tblTTS.Where(p => p.MaPhongBan == UserDepart).Select(p => p.MaTTS).ToList();

            bool hasData = DataProvider.Ins.DB.tblNhanSuDAs
            .Any(d => d.MaDA == maDADeleteHere && listTTSHere.Contains(d.MaTTS));

            if (!hasData)
            {
                var DACanXoaHere = DataProvider.Ins.DB.tblTrucThuocs.Where(p => p.MaDA == maDADeleteHere);


                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa Dự án này khỏi Phòng ban?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.tblTrucThuocs.RemoveRange(DACanXoaHere);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa Dự án khỏi Phòng ban thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    return;
                }
            } else
            {
             MessageBoxResult result = MessageBox.Show(
            "Vui lòng xóa toàn bộ nhân sự Thực tập sinh phòng ban đang quản lý khỏi Dự án trước khi xóa Dự án khỏi phòng ban!",
            "Cảnh báo",
             MessageBoxButton.OK,
             MessageBoxImage.Warning
                );
            }
        }

        public void XoaDuAn()
        {
            if (SelectedDA == null)
            {
                MessageBox.Show("Vui lòng chọn Dự án!");
                return;
            }

            var maDADelete = SelectedDA.MaDA;

            var DACanXoa = DataProvider.Ins.DB.tblDuAns.Where(p => p.MaDA == maDADelete);


            MessageBoxResult result = MessageBox.Show(
            "Bạn chắc chắn muốn xóa vĩnh viễn Dự án này? Hãy cân nhắc bởi điều này cũng sẽ xóa toàn bộ các thông tin và dữ liệu của Dự án ở các phòng ban khác (nếu có).",
            "Cảnh báo",
             MessageBoxButton.OKCancel,
                MessageBoxImage.Warning
                );

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblDuAns.RemoveRange(DACanXoa);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa Dự án thành công!");
            }
            else
            {
                return;
            }
        }
        }
    }

