using QuanLySinhVienThucTap.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using QuanLySinhVienThucTap.Model;
using System.Runtime.Remoting.Messaging;
using QuanLySinhVienThucTap.Pages.QuanLyNhanSuPage;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class QuanLyNhanSuViewModel : BaseViewModel
    {
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadDuAnCommand { get; set; }

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

        private ObservableCollection<NhiemVuCacDuAn> _ListNhiemVuDuAn;
        public ObservableCollection<NhiemVuCacDuAn> ListNhiemVuDuAn
        {
            get { return _ListNhiemVuDuAn; }
            set
            {
                _ListNhiemVuDuAn = value;
                OnPropertyChanged();
            }
        }

        public string maDA;

        public string UserDepart;
        public ICommand AddTTSToDuAn { get; set; }

        public ICommand AddTaskCommand { get; set; }

        public ICommand DeleteTTSDuAn { get; set; }

        public ICommand DeleteNhiemVuDA { get; set; }

        public ICommand SuaNhiemVuCommand { get; set; }

        public ICommand LocNhiemVuDACommand { get; set; }
        public ICommand BoLocNhiemVuDACommand { get; set; }
        public bool isOnLoaded = false;

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

        private NhiemVuCacDuAn _selectedNhiemVu;

        public NhiemVuCacDuAn SelectedNhiemVu
        {
            get { return _selectedNhiemVu; }
            set
            {
                _selectedNhiemVu = value;
                OnPropertyChanged(nameof(SelectedNhiemVu));
            }
        }

        private DateTime? _TuNgay;

        public DateTime? TuNgay
        {
            get { return _TuNgay; }
            set
            {
                _TuNgay = value;
                OnPropertyChanged(nameof(TuNgay));
            }
        }

        private DateTime? _DenNgay = DateTime.Now;

        public DateTime? DenNgay
        {
            get { return _DenNgay; }
            set
            {
                _DenNgay = value;
                OnPropertyChanged(nameof(DenNgay));
            }
        }

        private string _ProjectDisplayName;
        public string ProjectDisplayName { get => _ProjectDisplayName; set { _ProjectDisplayName = value; OnPropertyChanged(); } }
        public QuanLyNhanSuViewModel()
        {

            LoadDuAnCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {
                if (SelectedDuAn != null)
                {
                    maDA = SelectedDuAn.MaDA;
                    ProjectDisplayName = SelectedDuAn.TenDA;
                    LoadDuAn(maDA);
                }

            });

            AddTTSToDuAn = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    AddTTSToDuAn addttstoduan = new AddTTSToDuAn(maDA, UserDepart);
                    addttstoduan.ShowDialog();
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteTTSDuAn = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    DeleteTTS();
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            AddTaskCommand = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    if (SelectedItem == null)
                    {
                        MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Thực tập sinh.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    AddNhiemVuToDuAn addnhiemvutoduan = new AddNhiemVuToDuAn(maDA, SelectedItem.MaTTS, SelectedItem.TenTTS);
                    addnhiemvutoduan.ShowDialog();
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            SuaNhiemVuCommand = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    if (SelectedNhiemVu == null)
                    {
                        MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ cần cập nhật.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    SuaNhiemVuDuAn suaNhiemVu = new SuaNhiemVuDuAn(SelectedNhiemVu);
                    suaNhiemVu.ShowDialog();
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteNhiemVuDA = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    DeleteNhiemVuDuAn();
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            LocNhiemVuDACommand = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    LocDuAn();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            BoLocNhiemVuDACommand = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                if (isOnLoaded)
                {
                    TuNgay = null;
                    LoadDuAn(maDA);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
        public void OnLoaded()
        {

            ListDuAn = new ObservableCollection<CacDuAn>();

            var maDuAnList = DataProvider.Ins.DB.tblTrucThuocs
                .Where(tt => tt.MaPhongBan == UserDepart)
                .Select(tt => tt.MaDA)
                .ToList();
            var duanList = DataProvider.Ins.DB.tblDuAns
                    .Where(duAn => maDuAnList.Contains(duAn.MaDA))
                .ToList();
            foreach (var item in duanList)
            {
                CacDuAn cacduan = new CacDuAn();
                cacduan.MaDA = item.MaDA;
                cacduan.TenDA = item.TenDA;
                ListDuAn.Add(cacduan);
            }
        }

/*        public void LoadDuAn(string idDuAn)
        {
            NhanSuList = new ObservableCollection<NhanSuCacDuAn>();

            ListNhiemVuDuAn = new ObservableCollection<NhiemVuCacDuAn>();
            var nhansuList = DataProvider.Ins.DB.tblNhanSuDAs
                            .Where(ns => ns.MaDA == idDuAn)
                            .Join(
                             DataProvider.Ins.DB.tblTTS
                            .Where(tts => tts.MaPhongBan == UserDepart),
                             ns => ns.MaTTS,
                                tts => tts.MaTTS,
                                (ns, tts) => ns.MaTTS).ToList();

            var tblTTSList = DataProvider.Ins.DB.tblTTS
                            .Where(tts => nhansuList.Contains(tts.MaTTS))
                            .ToList();

            var nhiemvuList = DataProvider.Ins.DB.tblNhiemVuDAs
                                .Where(nv => nv.MaDA == idDuAn)
                                .Join(
                                DataProvider.Ins.DB.tblTTS
                                .Where(tts => tts.MaPhongBan == UserDepart),
                                nv => nv.MaTTS,
                                 tts => tts.MaTTS,
                                 (nv, tts) => nv
                                ).OrderByDescending(p => p.MaNhiemVuDA).ToList();

            foreach (var item in tblTTSList)
            {
                NhanSuCacDuAn nhansuduan = new NhanSuCacDuAn();
                nhansuduan.MaTTS = item.MaTTS;
                nhansuduan.TenTTS = item.TenTTS;
                nhansuduan.MaChucVu = DataProvider.Ins.DB.tblChucVus.Where(p => p.MaChucVu == item.MaChucVu).Select(p => p.TenChucVu).FirstOrDefault();
                nhansuduan.MaPhongBan = DataProvider.Ins.DB.tblPhongBans.Where(p => p.MaPhongBan == item.MaPhongBan).Select(p => p.TenPhongBan).FirstOrDefault();
                NhanSuList.Add(nhansuduan);
            }

            foreach (var item in nhiemvuList)
            {
                NhiemVuCacDuAn nhiemvucacduan = new NhiemVuCacDuAn();
                nhiemvucacduan.MaNhiemVuDA = item.MaNhiemVuDA.ToString();
                nhiemvucacduan.NhiemVu = item.NhiemVu;
                nhiemvucacduan.TenTTS = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == item.MaTTS).Select(p => p.TenTTS).FirstOrDefault();
                nhiemvucacduan.ChucVu = DataProvider.Ins.DB.tblTTS
                .Where(p => p.MaTTS == item.MaTTS)
                .Join(
                    DataProvider.Ins.DB.tblChucVus,
                    tts => tts.MaChucVu,
                    cv => cv.MaChucVu,
                    (tts, cv) => cv.TenChucVu)
                .FirstOrDefault(); ;
                nhiemvucacduan.Deadline = item.Deadline;
                nhiemvucacduan.NgayBatDau = item.NgayBatDau;
                nhiemvucacduan.Status = item.status;

                ListNhiemVuDuAn.Add(nhiemvucacduan);
            }
            isOnLoaded = true;
        }*/
        public void LoadDuAn(string idDuAn)
        {
            NhanSuList = new ObservableCollection<NhanSuCacDuAn>();
            ListNhiemVuDuAn = new ObservableCollection<NhiemVuCacDuAn>();

            var nhansuList = DataProvider.Ins.DB.tblNhanSuDAs
                .AsNoTracking()
                .Where(ns => ns.MaDA == idDuAn && ns.tblTT.MaPhongBan == UserDepart)
                .Select(ns => ns.MaTTS)
                .ToList();

            var tblTTSList = DataProvider.Ins.DB.tblTTS
                .AsNoTracking()
                .Where(tts => nhansuList.Contains(tts.MaTTS))
                .ToList();

            var nhiemvuList = DataProvider.Ins.DB.tblNhiemVuDAs
                .AsNoTracking()
                .Where(nv => nv.MaDA == idDuAn && nv.tblTT.MaPhongBan == UserDepart)
                .OrderByDescending(p => p.MaNhiemVuDA)
                .ToList();

            foreach (var item in tblTTSList)
            {
                NhanSuCacDuAn nhansuduan = new NhanSuCacDuAn();
                nhansuduan.MaTTS = item.MaTTS;
                nhansuduan.TenTTS = item.TenTTS;
                nhansuduan.MaChucVu = item.tblChucVu?.TenChucVu;
                nhansuduan.MaPhongBan = item.tblPhongBan?.TenPhongBan;
                NhanSuList.Add(nhansuduan);
            }

            foreach (var item in nhiemvuList)
            {
                NhiemVuCacDuAn nhiemvucacduan = new NhiemVuCacDuAn();
                nhiemvucacduan.MaNhiemVuDA = item.MaNhiemVuDA.ToString();
                nhiemvucacduan.NhiemVu = item.NhiemVu;
                nhiemvucacduan.TenTTS = item.tblTT?.TenTTS;
                nhiemvucacduan.ChucVu = item.tblTT?.tblChucVu?.TenChucVu;
                nhiemvucacduan.Deadline = item.Deadline;
                nhiemvucacduan.NgayBatDau = item.NgayBatDau;
                nhiemvucacduan.Status = item.status;
                ListNhiemVuDuAn.Add(nhiemvucacduan);
            }
            isOnLoaded = true;
        }

        public void DeleteTTS()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn nhân sự!");
                return;
            }

            string maTTSDelete = SelectedItem.MaTTS;

            var TTSCanXoa = DataProvider.Ins.DB.tblNhanSuDAs.Where(p => p.MaTTS == maTTSDelete && p.MaDA == SelectedDuAn.MaDA).ToList();
            var NhiemVuCanXoa = DataProvider.Ins.DB.tblNhiemVuDAs.Where(p => p.MaTTS == maTTSDelete && p.MaDA == SelectedDuAn.MaDA).ToList();

            if (TTSCanXoa.Any())
            {
                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa nhân sự khỏi dự án?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.tblNhanSuDAs.RemoveRange(TTSCanXoa);
                    if (NhiemVuCanXoa.Any())
                    {
                        DataProvider.Ins.DB.tblNhiemVuDAs.RemoveRange(NhiemVuCanXoa);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa Thực tập sinh thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    return;
                }
            }
        }
        public void DeleteNhiemVuDuAn()
        {
            if (SelectedNhiemVu == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ cần xóa.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int maNhiemVuDelete = int.Parse(SelectedNhiemVu.MaNhiemVuDA);

            var NhiemVuCanXoa = DataProvider.Ins.DB.tblNhiemVuDAs.Where(p => p.MaNhiemVuDA == maNhiemVuDelete).ToList();
            var NhanXetCanXoa = DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.Where(x => x.MaNhiemVuDA == maNhiemVuDelete).ToList();

            if (NhiemVuCanXoa.Any())
            {
                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa nhiệm vụ khỏi dự án?", "Xác nhận", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    DataProvider.Ins.DB.tblNhiemVuDAs.RemoveRange(NhiemVuCanXoa);
                    if (NhanXetCanXoa.Any())
                    {
                        DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.RemoveRange(NhanXetCanXoa);
                    }
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Xóa Nhiệm vụ thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    return;
                }

            }

        }
        public void LocDuAn()
        {

            ListNhiemVuDuAn = new ObservableCollection<NhiemVuCacDuAn>();
            if (TuNgay > DenNgay)
            {
                MessageBox.Show("Có lỗi xảy ra. Ngày bắt đầu phải trước Deadline.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TuNgay != null)
            {
                var nhiemvuList = DataProvider.Ins.DB.tblNhiemVuDAs
                                    .Where(nv => nv.MaDA == maDA && nv.NgayBatDau >= TuNgay && nv.Deadline <= DenNgay)
                                    .Join(
                                    DataProvider.Ins.DB.tblTTS
                                    .Where(tts => tts.MaPhongBan == UserDepart),
                                    nv => nv.MaTTS,
                                     tts => tts.MaTTS,
                                     (nv, tts) => nv
                                    ).OrderByDescending(p => p.MaNhiemVuDA).ToList();

                foreach (var item in nhiemvuList)
                {
                    NhiemVuCacDuAn nhiemvucacduan = new NhiemVuCacDuAn();
                    nhiemvucacduan.MaNhiemVuDA = item.MaNhiemVuDA.ToString();
                    nhiemvucacduan.NhiemVu = item.NhiemVu;
                    nhiemvucacduan.TenTTS = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == item.MaTTS).Select(p => p.TenTTS).FirstOrDefault();
                    nhiemvucacduan.ChucVu = DataProvider.Ins.DB.tblTTS
                    .Where(p => p.MaTTS == item.MaTTS)
                    .Join(
                        DataProvider.Ins.DB.tblChucVus,
                        tts => tts.MaChucVu,
                        cv => cv.MaChucVu,
                        (tts, cv) => cv.TenChucVu)
                    .FirstOrDefault(); ;
                    nhiemvucacduan.Deadline = item.Deadline;
                    nhiemvucacduan.NgayBatDau = item.NgayBatDau;
                    nhiemvucacduan.Status = item.status;
                    ListNhiemVuDuAn.Add(nhiemvucacduan);
                }
            } else
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng chọn ngày bắt đầu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadDuAn(maDA);
            }
            isOnLoaded = true;
        }
    }
}
