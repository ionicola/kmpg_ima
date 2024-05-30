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
    public class ThemDuAnViewModel : BaseViewModel
    {
        public string UserDepart;
        private string _maDuAn;

        public string maDuAn
        {
            get { return _maDuAn; }
            set
            {
                _maDuAn = value;
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
        private ObservableCollection<CacDuAn> _DuAnList;
        public ObservableCollection<CacDuAn> DuAnList
        {
            get { return _DuAnList; }
            set
            {
                _DuAnList = value;
                OnPropertyChanged();
            }
        }

        private CacDuAn _selectedItem;

        public CacDuAn SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand ThemDuAnCommand { get; set; }

        public ICommand LoadWindowCommand { get; set; }

        public ThemDuAnViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadDuAn();
            });
            ThemDuAnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ThemDuAn();
            });
        }

        public void LoadDuAn()
        {
            DuAnList = new ObservableCollection<CacDuAn>();

            var maDuAnTrongPhongBan = DataProvider.Ins.DB.tblTrucThuocs.Where(p => p.MaPhongBan == UserDepart).Select(p => p.MaDA).ToList();
            var allDuAnData = DataProvider.Ins.DB.tblDuAns
                .Where(tts => !maDuAnTrongPhongBan.Contains(tts.MaDA))
                .ToList();
            PhongBan = DataProvider.Ins.DB.tblPhongBans.Where(p => p.MaPhongBan == UserDepart).Select(p => p.MaPhongBan).FirstOrDefault();
            foreach (var item in allDuAnData)
            {
                CacDuAn duan = new CacDuAn();
                duan.MaDA = item.MaDA;
                duan.TenDA = item.TenDA;
                DuAnList.Add(duan);
            }
        }

        public void ThemDuAn()
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Dự án cần thêm vào Phòng ban.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            tblTrucThuoc newTT = new tblTrucThuoc
            {
                MaDA = SelectedItem.MaDA,
                MaPhongBan = UserDepart
            };

            var existingRecord = DataProvider.Ins.DB.tblTrucThuocs
            .Where(ns => ns.MaDA == newTT.MaDA && ns.MaPhongBan == newTT.MaPhongBan)
            .FirstOrDefault();

            if (existingRecord == null)
            {
                DataProvider.Ins.DB.tblTrucThuocs.Add(newTT);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm Dự án vào Phòng ban thành công.", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bản ghi đã tồn tại. Bạn đã thêm Dự án vào phòng ban trước đó. Vui lòng thoát để kiểm tra.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
