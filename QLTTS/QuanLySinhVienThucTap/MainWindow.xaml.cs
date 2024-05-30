using QuanLySinhVienThucTap.UserControllerNEU;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLySinhVienThucTap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string id)
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel();
            viewModel.UserId = id.ToUpper();
            DataContext = viewModel;
            gridQuanLy.DataContext = viewModel;
        }

        private void ListBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        /*        private void ListBoxItem1_Click(object sender, RoutedEventArgs e)
                {
                    if (currentUserControl == null)
                    {
                        currentUserControl = new ManageProject_ManageProjectPerson();
                    }
                    contentFrame.Content = currentUserControl;
                }
                private void ListBoxItem2_Click(object sender, RoutedEventArgs e)
                {
                    contentFrame.Content = new UserControl2();
                }
                private void ListBoxItem3_Click(object sender, RoutedEventArgs e)
                {
                    contentFrame.Content = new UserControl2();
                }
                private void ListViewItem_Selected(object sender, RoutedEventArgs e)
                {

                }*/
    }
}

     
