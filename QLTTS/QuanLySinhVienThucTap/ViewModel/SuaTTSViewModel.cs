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
    public class SuaTTSViewModel : BaseViewModel
    {
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

        private string _TenTTS;
        public string TenTTS
        {
            get { return _TenTTS; }
            set
            {
                _TenTTS = value;
                OnPropertyChanged(nameof(TenTTS));
            }
        }
        
        private string _MaTTS;
        public string MaTTS
        {
            get { return _MaTTS; }
            set
            {
                _MaTTS = value;
                OnPropertyChanged(nameof(MaTTS));
            }
        }

        private string _ChucVu = "Thực tập sinh";

        public string ChucVu
        {
            get { return _ChucVu; }
            set
            {
                _ChucVu = value;
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

        private tblPhongBan _SelectedPhongBan;

        public tblPhongBan SelectedPhongBan
        {
            get { return _SelectedPhongBan; }
            set
            {
                _SelectedPhongBan = value;
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
        public ICommand SuaTTSDuAnCommand { get; set; }

        public ICommand LoadWindowCommand { get; set; }

        public SuaTTSViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadWindow();
            });

            SuaTTSDuAnCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SuaTTS();
            });

        }

        public int index;
        public void LoadWindow()
        {
            PhongBanList = new ObservableCollection<tblPhongBan>();
            TenTTS = SelectedTTS.TenTTS;
            MaTTS = SelectedTTS.MaTTS;
            ChucVu = ChucVu;
            PhongBan = SelectedTTS.MaPhongBan;
            string MaPhongBan = DataProvider.Ins.DB.tblTTS.Where(p => p.MaTTS == SelectedTTS.MaTTS).Select(p => p.MaPhongBan).FirstOrDefault();

            var PhongBanlist = DataProvider.Ins.DB.tblPhongBans.ToList();
            int j = -1;
            foreach (var item in PhongBanlist)
            {
                tblPhongBan newPhongBanList = new tblPhongBan();
                newPhongBanList.MaPhongBan = item.MaPhongBan;
                newPhongBanList.TenPhongBan = item.TenPhongBan;
                PhongBanList.Add(newPhongBanList);
                j++;
                if (item.MaPhongBan.Equals(MaPhongBan))
                {
                    index = j;
                }
            }
        }

            public void SuaTTS()
        {
            var recordToUpdate = DataProvider.Ins.DB.tblTTS.FirstOrDefault(p => p.MaTTS == SelectedTTS.MaTTS);

            if (recordToUpdate != null)
            {
                recordToUpdate.MaPhongBan = SelectedPhongBan.MaPhongBan;
                recordToUpdate.TenTTS = TenTTS;
                DataProvider.Ins.DB.SaveChanges();
            }

            MessageBox.Show("Sửa thông tin Thực tập sinh thành công!");
        }
    }
}
