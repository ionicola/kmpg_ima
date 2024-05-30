using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Security.Cryptography;
using System.Collections.ObjectModel;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {   
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get=>_UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private ObservableCollection<LoginMode> _loginModes;
        public ObservableCollection<LoginMode> LoginModes
        {
            get { return _loginModes; }
            set
            {
                _loginModes = value;
                OnPropertyChanged(nameof(LoginModes));
            }
        }

        private string _selectedLoginMode;
        public string SelectedLoginMode
        {
            get { return _selectedLoginMode; }
            set
            {
                _selectedLoginMode = value;
                OnPropertyChanged(nameof(SelectedLoginMode));
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand Quit { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;
            UserName = "";
            Password = "";
            LoginModes = new ObservableCollection<LoginMode>
        {
            new LoginMode { Name = "Quản lý", Value = "QL" },
            new LoginMode { Name = "Thực tập sinh", Value = "TTS" },
            new LoginMode { Name = "Hành chính Nhân sự", Value = "NS" },
        };

            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });

            Quit = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng chương trình?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            });
        }

        void Login(Window p)
        {
            if (p == null)
            {
                return;
            }
            string PassEncode = MD5Hash(Base64Encode(Password));
            if (SelectedLoginMode == "QL")
            {
                var accCount = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserName && x.MatKhau == PassEncode).Count();

                if (accCount == 1)
                {
                    IsLogin = true;
                    MainWindow mainWindow = new MainWindow(UserName);
                    mainWindow.Show();
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại thông tin đăng nhập.");
                }
            } 
            else if (SelectedLoginMode == "TTS") {
                var accCount = DataProvider.Ins.DB.tblTTS.Where(x => x.MaTTS == UserName && x.MatKhau == PassEncode).Count();

                if (accCount == 1)
                {
                    IsLogin = true;
                    TTS_MainWindow TTSmainWindow = new TTS_MainWindow(UserName);
                    TTSmainWindow.Show();
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại thông tin đăng nhập.");
                }
            }
            else if (SelectedLoginMode == "NS") {
                var accCount = DataProvider.Ins.DB.tblQLs.Where(x => x.MaQL == UserName && x.MatKhau == PassEncode && x.MaPhongBan == "NS").Count();
                if (accCount == 1)
                {
                    IsLogin = true;
                    NS_MainWindow ns_MainWindow = new NS_MainWindow(UserName);
                    ns_MainWindow.Show();
                    p.Close();
                }
                else
                {
                    IsLogin = false;
                    MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại thông tin đăng nhập.");
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Kiểm tra lại thông tin đăng nhập.");
            }
        }

        public string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
    public class LoginMode
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
