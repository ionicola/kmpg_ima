using QuanLySinhVienThucTap.ViewModel;
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

namespace QuanLySinhVienThucTap.Pages.QuanLyChungPage
{
    public partial class SuaTTS : Window
    {
        public SuaTTSViewModel suaTTSVM = new SuaTTSViewModel();
        public SuaTTS(Model.NhanSuCacDuAn selectedTTS)
        {
            InitializeComponent();
            suaTTSVM.SelectedTTS = selectedTTS;
            DataContext = suaTTSVM;
        }
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            int index = suaTTSVM.index;
            myComboBox.SelectedItem = myComboBox.Items[index];
        }
    }
}
