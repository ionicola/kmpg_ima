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
    public class TaoMoiTTSViewModel : BaseViewModel
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
                _PhongBan= value;
                OnPropertyChanged();
            }
        }

        private string _MaChucVu;

        public string MaChucVu
        {
            get { return _MaChucVu; }
            set
            {
                _MaChucVu = value;
                OnPropertyChanged();
            }
        }
        private string _MaPhongBan;

        public string MaPhongBan
        {
            get { return _MaPhongBan; }
            set
            {
                _MaPhongBan = value;
                OnPropertyChanged();
            }
        }
        private string _generateMaTTS;

        public string generateMaTTS
        {
            get { return _generateMaTTS; }
            set
            {
                _generateMaTTS = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand ThemMoiTTSCommand { get; set; }
        public TaoMoiTTSViewModel()
        {
            LoadWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                GenMaTTS();
            });

            ThemMoiTTSCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddNewTTS();
            });
        }

        public void GenMaTTS()
        {
            var randomNumber = new Random().Next(1, 1001);
            generateMaTTS = MaPhongBan + randomNumber;
            while (DataProvider.Ins.DB.tblTTS.Any(item => item.MaTTS == generateMaTTS))
            {
                randomNumber = new Random().Next(1, 1001);
                generateMaTTS = MaPhongBan + randomNumber;
            }
            MaTTS = generateMaTTS;
            PhongBan = DataProvider.Ins.DB.tblPhongBans.Where(p => p.MaPhongBan == MaPhongBan).Select(p => p.TenPhongBan).FirstOrDefault();
            ChucVu = ChucVu;
        }
        public void AddNewTTS()
        {
            if(DataProvider.Ins.DB.tblTTS.Any(item => item.MaTTS == generateMaTTS))
            {
                MessageBox.Show("Thực tập sinh đã tồn tại. Vui lòng đóng cửa sổ để kiểm tra.");
                return;
            }
            if (TenTTS == null)
            {
                MessageBox.Show("Chưa nhập tên Thực tập sinh.");
                return;
            }

            tblTT newTTS = new tblTT
            {
                MaTTS = generateMaTTS,
                TenTTS = TenTTS,
                MaPhongBan = MaPhongBan,
                MaChucVu = "TTS",
                MatKhau = "db69fc039dcbd2962cb4d28f5891aae1"
            };

            string message = $"Bạn có chắc chắn muốn thêm thực tập sinh này không?\n" +
                $"Mã Thực tập sinh: {generateMaTTS}\n" +
                 $"Tên Thực tập sinh: {TenTTS}\n" +
                 $"Phòng Ban: {PhongBan}\n" +
                 $"Chức Vụ: Thực tập sinh";

            MessageBoxResult result = MessageBox.Show(message, "Xác nhận", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                DataProvider.Ins.DB.tblTTS.Add(newTTS);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm thực tập sinh thành công!");
            }
            else
            {
                return;
            }
        }
    }
    }

