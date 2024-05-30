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
    public class TTS_KetQuaKhoaHocViewModel : BaseViewModel
    {
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
        public ICommand LoadKhoaHocCommand { get; set; }
        public ICommand CompleteCommand { get; set; }
        public ICommand DetailsCommand { get; set; }

        private TTS_NhiemVuDaoTao _selectedKhoaHoc;

        public TTS_NhiemVuDaoTao SelectedKhoaHoc
        {
            get { return _selectedKhoaHoc; }
            set
            {
                _selectedKhoaHoc = value;
                OnPropertyChanged(nameof(SelectedKhoaHoc));
            }
        }

        private ObservableCollection<TTS_NhiemVuDaoTao> _ListKhoaHoc;
        public ObservableCollection<TTS_NhiemVuDaoTao> ListKhoaHoc
        {
            get { return _ListKhoaHoc; }
            set
            {
                _ListKhoaHoc = value;
                OnPropertyChanged(nameof(ListKhoaHoc));
            }
        }
        private ObservableCollection<TTS_NhiemVuCacDuAn> _ListNhiemVu;
        public ObservableCollection<TTS_NhiemVuCacDuAn> ListNhiemVu
        {
            get { return _ListNhiemVu; }
            set
            {
                _ListNhiemVu = value;
                OnPropertyChanged(nameof(ListNhiemVu));
            }
        }

        private string _tenKhoa;

        public string TenKhoa
        {
            get { return _tenKhoa; }
            set
            {
                _tenKhoa = value;
                OnPropertyChanged(nameof(TenKhoa));
            }
        }

        public TTS_KetQuaKhoaHocViewModel()
        {
            LoadKhoaHocCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadNhiemVuObj();

            });
        }
        public void LoadNhiemVuObj()
        {
            ListKhoaHoc = new ObservableCollection<TTS_NhiemVuDaoTao>();
            var KhoaHocList = DataProvider.Ins.DB.tblNhiemVuDaoTaos.Where(p => p.MaTTS == UserId && p.status != "in-progress").OrderByDescending(p => p.Deadline).ToList();
            int i = 1;
            foreach (var item in KhoaHocList)
            {
                TTS_NhiemVuDaoTao cacnv = new TTS_NhiemVuDaoTao();
                cacnv.MaNhiemVuDaoTao = item.MaNhiemVuDaoTao;
                cacnv.MaKhoaDaoTao = item.MaKhoaDaoTao;
                cacnv.Deadline = item.Deadline;
                cacnv.TenKhoa = DataProvider.Ins.DB.tblKhoaDaoTaos.Where(p => p.MaKhoaDaoTao == item.MaKhoaDaoTao).Select(p => p.TenKhoa).SingleOrDefault();
                cacnv.STT = i;
                cacnv.Status = item.status;
                ListKhoaHoc.Add(cacnv);
                i++;
            }
        }
    }
}
