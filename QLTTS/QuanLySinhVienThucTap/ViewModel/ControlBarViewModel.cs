using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand closeWindowCommand { get; set; }
        #endregion
        public ICommand versionWindowCommand { get; set; }

        public ICommand LogoutCommand { get; set; }
        public ControlBarViewModel()
        {
            closeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            });
            versionWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                string version = "1.0.2";
                MessageBox.Show($"Phiên bản: {version}", "Thông tin phiên bản", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            LogoutCommand = new RelayCommand<UserControl>((p) => { return CanLogout(p); }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn đăng xuất tài khoản", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if (w != null)
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Loaded += (sender, e) =>
                        {
                            Application.Current.MainWindow = loginWindow;
                        };
                        loginWindow.Show();
                        w.Close();
                    }
                }
            });

        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (p.Parent != null)
            {
                FrameworkElement previousParent = parent;
                parent = parent.Parent as FrameworkElement;
                if (parent == null)
                {
                    parent = previousParent;
                    break;
                }
            }
            return parent;
        }
        private bool CanLogout(UserControl userControl)
        {
            FrameworkElement window = GetWindowParent(userControl);
            var w = window as Window;
            return w == null || !(w is LoginWindow);
        }
    }
}
