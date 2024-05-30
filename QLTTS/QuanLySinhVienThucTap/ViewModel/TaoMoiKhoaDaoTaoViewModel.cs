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
    public class TaoMoiKhoaDaoTaoViewModel : BaseViewModel
    {
        private string _TenKhoaDaoTao;

        public string TenKhoaDaoTao
        {
            get { return _TenKhoaDaoTao; }
            set
            {
                _TenKhoaDaoTao = value;
                OnPropertyChanged();
            }
        }

        private string _MaKhoaDaoTao;

        public string MaKhoaDaoTao
        {
            get { return _MaKhoaDaoTao; }
            set
            {
                _MaKhoaDaoTao = value;
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

        private string _generateMaKhoa;

        public string generateMaKhoa
        {
            get { return _generateMaKhoa; }
            set
            {
                _generateMaKhoa = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ComboBoxItemWithTag> _loaiKhoaDaoTao;

        public ObservableCollection<ComboBoxItemWithTag> LoaiKhoaDaoTao
        {
            get { return _loaiKhoaDaoTao; }
            set
            {
                _loaiKhoaDaoTao = value;
                OnPropertyChanged(nameof(LoaiKhoaDaoTao));
            }
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand ThemMoiKhoaDaoTaoCommand { get; set; }

        public string SelectedItemId;
        public TaoMoiKhoaDaoTaoViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var chungItem = new ComboBoxItemWithTag { DisplayName = "Chung", Tag = "C" };
                var phongBanItem = new ComboBoxItemWithTag { DisplayName = DataProvider.Ins.DB.tblPhongBans.Where(x=>x.MaPhongBan == PhongBan).Select(x => x.TenPhongBan).FirstOrDefault(),
                    Tag = PhongBan };

                LoaiKhoaDaoTao = new ObservableCollection<ComboBoxItemWithTag> { chungItem, phongBanItem };
            });


                ThemMoiKhoaDaoTaoCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddNewKhoaDaoTao();
            });


        }

        public void GenMaKhoaDaoTao()
        {
            var randomNumber = new Random().Next(1, 1001);
            generateMaKhoa = "DT" + SelectedItemId  + randomNumber;
            while (DataProvider.Ins.DB.tblKhoaDaoTaos.Any(item => item.MaKhoaDaoTao == generateMaKhoa))
            {
                randomNumber = new Random().Next(1, 1001);
                generateMaKhoa = "DT" + SelectedItemId + randomNumber;
            }
            MaKhoaDaoTao = generateMaKhoa;
        }
        public void AddNewKhoaDaoTao()
        {
            if (DataProvider.Ins.DB.tblKhoaDaoTaos.Any(item => item.MaKhoaDaoTao == generateMaKhoa))
            {
                MessageBox.Show("Khóa đào tạo đã tồn tại. Vui lòng đóng cửa sổ để kiểm tra.");
                return;
            }
            if (TenKhoaDaoTao == null)
            {
                MessageBox.Show("Chưa nhập tên Khóa đào tạo.");
                return;
            }

            tblKhoaDaoTao newKhoaDaoTao = new tblKhoaDaoTao
            {
                MaKhoaDaoTao = generateMaKhoa ,
                TenKhoa = TenKhoaDaoTao
                    };

            string message = $"Bạn có chắc chắn muốn thêm khóa đào tạo này vào hệ thống không?\n" +
                $"Mã Khóa đào tạo: {generateMaKhoa}\n" +
                 $"Tên Khóa đào tạo: {TenKhoaDaoTao}\n";

            MessageBoxResult result = MessageBox.Show(message, "Xác nhận", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblKhoaDaoTaos.Add(newKhoaDaoTao);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm Khóa đào tạo thành công!");
            }
            else
            {
                return;
            }
        }
    }
    public class ComboBoxItemWithTag
    {
        public string DisplayName { get; set; }
        public string Tag { get; set; }
    }
}
