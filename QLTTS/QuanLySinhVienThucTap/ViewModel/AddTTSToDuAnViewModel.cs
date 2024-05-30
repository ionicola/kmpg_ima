using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages.QuanLyNhanSuPage;
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

    public class AddTTSToDuAnViewModel : BaseViewModel
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
        private ObservableCollection<NhanSuCacDuAn> _NhanSuList;
        public ObservableCollection<NhanSuCacDuAn> NhanSuList
        {
            get { return _NhanSuList; }
            set
            {
                _NhanSuList = value;
                OnPropertyChanged();
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

        public bool isAdded = false;
        public ICommand AddTTSToDuAnDetail { get; set; }

        public ICommand LoadWindowCommand { get; set; }
        public AddTTSToDuAnViewModel()
        {

            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadDuAn();
            });

            AddTTSToDuAnDetail = new RelayCommand<object>((x) => { return true; }, (x) =>
            {
                AddTTS();
            });
        }

        public void LoadDuAn()
        {
            NhanSuList = new ObservableCollection<NhanSuCacDuAn>();

            var maNhanSuTrongDuAn = DataProvider.Ins.DB.tblNhanSuDAs.Where(p => p.MaDA == maDuAn).Select(p => p.MaTTS).ToList();

            var allTTSData = DataProvider.Ins.DB.tblTTS
                .Where(tts => !maNhanSuTrongDuAn.Contains(tts.MaTTS) && tts.MaPhongBan == UserDepart)
                .ToList();

            foreach (var ttsData in allTTSData)
            {
                NhanSuCacDuAn nhansuduan = new NhanSuCacDuAn();
                nhansuduan.MaTTS = ttsData.MaTTS;
                nhansuduan.TenTTS = ttsData.TenTTS;

                nhansuduan.MaChucVu = ttsData.tblChucVu?.TenChucVu;
                nhansuduan.MaPhongBan = ttsData.tblPhongBan?.TenPhongBan;

                NhanSuList.Add(nhansuduan);
            }
        }

        public void AddTTS()
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Thực tập sinh cần thêm.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            tblNhanSuDA newTTStoDuAn = new tblNhanSuDA
            {
                MaTTS = SelectedItem.MaTTS,
                MaDA = maDuAn
            };

            var existingRecord = DataProvider.Ins.DB.tblNhanSuDAs
            .Where(ns => ns.MaDA == newTTStoDuAn.MaDA && ns.MaTTS == newTTStoDuAn.MaTTS)
            .FirstOrDefault();

            if (existingRecord == null)
            {
                DataProvider.Ins.DB.tblNhanSuDAs.Add(newTTStoDuAn);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm Thực tập sinh thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bản ghi đã tồn tại. Bạn đã thêm thực tập sinh trước đó. Vui lòng thoát để kiểm tra.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
