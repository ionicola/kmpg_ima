using QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class ChiTietVaNhanXetCongViecVM : BaseViewModel
    {
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadDuAnCommand { get; set; }
        private int? _danhGia;

        public int? DanhGia
        {
            get { return _danhGia; }
            set
            {
                if (_danhGia != value)
                {
                    _danhGia = value;
                    OnPropertyChanged();
                }
            }
        }

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
        public string maDA;
        public string UserDepart;

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
        public bool isOnLoaded = false;
        public ICommand SelectedNVCommand { get; set; }
        public ICommand SendNhanXet { get; set; }
        public ICommand LocTaskTTS { get; set; }

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
        private string _TenTTS1;

        public string TenTTS1
        {
            get { return _TenTTS1; }
            set
            {
                _TenTTS1 = value;
                OnPropertyChanged();
            }
        }

        private string _MaNhiemVu1;

        public string MaNhiemVu1
        {
            get { return _MaNhiemVu1; }
            set
            {
                _MaNhiemVu1 = value;
                OnPropertyChanged();
            }
        }
        private string _NhiemVu1;

        public string NhiemVu1
        {
            get { return _NhiemVu1; }
            set
            {
                _NhiemVu1 = value;
                OnPropertyChanged();
            }
        }
        private string _Deadline1;

        public string Deadline1
        {
            get { return _Deadline1; }
            set
            {
                _Deadline1 = value;
                OnPropertyChanged();
            }
        }
        private string _Status1;

        public string Status1
        {
            get { return _Status1; }
            set
            {
                _Status1 = value;
                OnPropertyChanged();
            }
        }
        private string _NhanXetNhiemVu;
        public string NhanXetNhiemVu
        {
            get { return _NhanXetNhiemVu; }
            set
            {
                if (_NhanXetNhiemVu != value)
                {
                    _NhanXetNhiemVu = value;
                    OnPropertyChanged(nameof(NhanXetNhiemVu));
                }
            }
        }
        public ChiTietVaNhanXetCongViecVM() 
        {
            LoadDuAnCommand = new RelayCommand<ListBox>((p) => { return true; }, (p) =>
            {
                if (SelectedDuAn != null)
                {
                    LoadDuAn(SelectedDuAn.MaDA);
                }

            });

            SelectedNVCommand = new RelayCommand<ListViewItem>((p) => { return true; }, (p) =>
            {
                LoadNhanXetForm();
            });

            SendNhanXet = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedDuAn == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if(SelectedNhiemVu == null) {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ cần nhận xét.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    AddNhanXet();
            });
            LocTaskTTS = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedDuAn == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (SelectedNhiemVu == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ bất kỳ của Thực tập sinh cần lọc.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    var idNhiemVu = int.Parse(SelectedNhiemVu?.MaNhiemVuDA);
                    var idTTS = DataProvider.Ins.DB.tblNhiemVuDAs.Where(x => x.MaNhiemVuDA == idNhiemVu).Select(z => z.MaTTS).First();
                    maDA = SelectedDuAn.MaDA;
                    LocNhiemVu(maDA, idTTS);
            });
        }

        public void LoadNhanXetForm()
        {
            TenTTS1 = SelectedNhiemVu?.TenTTS;
            MaNhiemVu1 = SelectedNhiemVu?.MaNhiemVuDA;
            NhiemVu1 = SelectedNhiemVu?.NhiemVu;
            Deadline1 = SelectedNhiemVu?.Deadline.ToString();
            NhanXetNhiemVu = DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.Where(x => x.MaNhiemVuDA.ToString() == SelectedNhiemVu.MaNhiemVuDA).Select(x => x.NhanXet).SingleOrDefault();
            Status1 = SelectedNhiemVu?.Status;
            DanhGia = DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.Where(x => x.MaNhiemVuDA.ToString() == SelectedNhiemVu.MaNhiemVuDA).Select(x => x.Diem).SingleOrDefault();
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

        public void LoadDuAn(string idDuAn)
        {
            ListNhiemVuDuAn = new ObservableCollection<NhiemVuCacDuAn>();

            var nhiemvuList = DataProvider.Ins.DB.tblNhiemVuDAs
                                .Where(nv => nv.MaDA == idDuAn)
                                .Join(
                                DataProvider.Ins.DB.tblTTS
                                .Where(tts => tts.MaPhongBan == UserDepart),
                                nv => nv.MaTTS,
                                 tts => tts.MaTTS,
                                 (nv, tts) => nv
                                ).OrderByDescending(p=>p.MaNhiemVuDA).ToList();

            foreach (var item in nhiemvuList)
            {

                var chucvu = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == item.MaTTS).Select(p => p.MaChucVu).ToList()[0];
                chucvu = DataProvider.Ins.DB.tblChucVus.Where(p => p.MaChucVu == chucvu).Select(p => p.TenChucVu).ToList()[0];
                var nhansu = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == item.MaTTS).Select(p => p.TenTTS).ToList()[0];

                NhiemVuCacDuAn nhiemvucacduan = new NhiemVuCacDuAn();
                nhiemvucacduan.MaNhiemVuDA = item.MaNhiemVuDA.ToString();
                nhiemvucacduan.NhiemVu = item.NhiemVu;
                nhiemvucacduan.TenTTS = nhansu;
                nhiemvucacduan.ChucVu = chucvu;
                nhiemvucacduan.Deadline = item.Deadline;
                nhiemvucacduan.NgayBatDau = item.NgayBatDau;
                nhiemvucacduan.Status = item.status;

                ListNhiemVuDuAn.Add(nhiemvucacduan);
            }
            isOnLoaded = true;
        }

        public void AddNhanXet()
        {
           /* if (NhanXetNhiemVu == null || NhanXetNhiemVu.Trim().Length < 10)
            {
                MessageBox.Show("Nhận xét quá ngắn. Vui lòng nhập lại.");
                return;
            } else*/ if (DanhGia == 0)
            {
                MessageBox.Show("Vui lòng chọn sao đánh giá.");
                return;
            }

            tblNhanXetNhiemVuDA newnhanxet = new tblNhanXetNhiemVuDA
            {
                MaNhiemVuDA = int.Parse(MaNhiemVu1),
                NhanXet = NhanXetNhiemVu,
                Diem = DanhGia
                
            };

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm/cập nhật nhận xét không?", "Xác nhận", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                var existingNhanXet = DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.SingleOrDefault(x => x.MaNhiemVuDA == newnhanxet.MaNhiemVuDA);

                if (existingNhanXet != null)
                {
                    existingNhanXet.NhanXet = newnhanxet.NhanXet;
                    existingNhanXet.Diem = newnhanxet.Diem;
                    DataProvider.Ins.DB.SaveChanges();

                    MessageBox.Show("Cập nhật nhận xét thành công!");
                    LoadNhanXetForm();
                }
                else
                {
                    DataProvider.Ins.DB.tblNhanXetNhiemVuDAs.Add(newnhanxet);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm nhận xét thành công!");
                    SelectedNhiemVu = null;
                    LoadNhanXetForm();
                }
            }
            else
            {
                MessageBox.Show("Đã hủy");
                SelectedNhiemVu = null;
                LoadNhanXetForm();
                
            }
        }

        public void LocNhiemVu(string idDuAn, string idTTS)
        {
            ListNhiemVuDuAn = new ObservableCollection<NhiemVuCacDuAn>();

            var nhiemvuList = DataProvider.Ins.DB.tblNhiemVuDAs.Where(p => p.MaDA == idDuAn && p.MaTTS == idTTS).ToList();

            foreach (var item in nhiemvuList)
            {
                var maNhiemVu = item.MaNhiemVuDA.ToString();
                var nhiemvu = item.NhiemVu.ToString();
                var nhansu = item.MaTTS.ToString();

                var chucvu = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == nhansu).Select(p => p.MaChucVu).ToList()[0];
                chucvu = DataProvider.Ins.DB.tblChucVus.Where(p => p.MaChucVu == chucvu).Select(p => p.TenChucVu).ToList()[0];
                nhansu = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == nhansu).Select(p => p.TenTTS).ToList()[0];

                var deadline = item.Deadline.Value;
                var ngaybatdau = item.NgayBatDau.Value;
                var tiendo = item.status.ToString();

                NhiemVuCacDuAn nhiemvucacduan = new NhiemVuCacDuAn();
                nhiemvucacduan.MaNhiemVuDA = maNhiemVu;
                nhiemvucacduan.NhiemVu = nhiemvu;
                nhiemvucacduan.TenTTS = nhansu;
                nhiemvucacduan.ChucVu = chucvu;
                nhiemvucacduan.Deadline = deadline;
                nhiemvucacduan.NgayBatDau = ngaybatdau;
                nhiemvucacduan.Status = tiendo;
                ListNhiemVuDuAn.Add(nhiemvucacduan);
            }
            isOnLoaded = true;
        }
    }
}
