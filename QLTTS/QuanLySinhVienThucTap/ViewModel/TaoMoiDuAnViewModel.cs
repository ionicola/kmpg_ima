using QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class TaoMoiDuAnViewModel : BaseViewModel
    {
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
        private string _UserId;

        public string UserId
        {
            get { return _UserId; }
            set
            {
                _UserId = value;
                OnPropertyChanged();
            }
        }
        private string _MaDA;

        public string MaDA
        {
            get { return _MaDA; }
            set
            {
                _MaDA = value;
                OnPropertyChanged();
            }
        }
        private string _TenDA;

        public string TenDA
        {
            get { return _TenDA; }
            set
            {
                _TenDA = value;
                OnPropertyChanged();
            }
        }

        private string _generateMaDA;

        public string generateMaDA
        {
            get { return _generateMaDA; }
            set
            {
                _generateMaDA = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand ThemMoiDuAnCommand { get; set; }
        public TaoMoiDuAnViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                GenMaDA();
            });

            ThemMoiDuAnCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddNewDA();
            });
        }

        public void GenMaDA()
        {
            var randomNumber = new Random().Next(1, 1001);
            generateMaDA = "DA" + randomNumber;
            while (DataProvider.Ins.DB.tblDuAns.Any(item => item.MaDA == generateMaDA))
            {
                randomNumber = new Random().Next(1, 1001);
                generateMaDA = "DA" + randomNumber;
            }
            MaDA = generateMaDA;
            PhongBan = DataProvider.Ins.DB.tblPhongBans.Where(p => p.MaPhongBan == DataProvider.Ins.DB.tblQLs.Where(x=>x.MaQL == UserId).Select(x=>x.MaPhongBan).FirstOrDefault()).Select(p => p.TenPhongBan).FirstOrDefault();
        }
        public void AddNewDA()
        {
            if (DataProvider.Ins.DB.tblTTS.Any(item => item.MaTTS == generateMaDA))
            {
                MessageBox.Show("Dự án đã tồn tại. Vui lòng đóng cửa sổ để kiểm tra.");
                return;
            }
            if (TenDA == null)
            {
                MessageBox.Show("Chưa nhập tên Dự án.");
                return;
            }

            tblDuAn newDA = new tblDuAn
            {
                MaDA = generateMaDA,
                TenDA = TenDA
            };
            tblTrucThuoc newTT = new tblTrucThuoc
            {
                MaDA = generateMaDA,
                MaPhongBan = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserId).Select(x => x.MaPhongBan).FirstOrDefault()
            };

            string _messageAddProject = $"Bạn có chắc chắn muốn thêm Dự án này không?\n" +
                $"Mã Dự án: {generateMaDA}\n" +
                 $"Tên Dự án: {TenDA}\n" +
                 $"Phòng Ban: {PhongBan}\n";

            MessageBoxResult result = MessageBox.Show(_messageAddProject, "Xác nhận", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblDuAns.Add(newDA);
                DataProvider.Ins.DB.tblTrucThuocs.Add(newTT);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Tạo mới Dự án thành công!");
            }
            else
            {
                MessageBox.Show("Đã hủy");
            }
        }
    }
}
