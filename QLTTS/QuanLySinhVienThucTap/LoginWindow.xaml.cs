using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Model.QuanLySinhVienThucTap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLySinhVienThucTap
{
    public partial class LoginWindow : Window
    {
        public UpdateDb updateDB = new UpdateDb();
        public LoginWindow()
        {
            InitializeComponent();
            updateDB.UpdateStatusBasedOnDeadline();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ControlBarUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
