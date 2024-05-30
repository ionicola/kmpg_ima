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
using System.Windows.Threading;


namespace QuanLySinhVienThucTap.TTS_Page
{
    /// <summary>
    /// Interaction logic for TTS_DiemDanhVaChamCong.xaml
    /// </summary>
    public partial class TTS_DiemDanhVaChamCong : Page
    {
        private DispatcherTimer timer;
        public TTS_DiemDanhVaChamCong(string UserId)
        {
            InitializeComponent();
            // Khởi tạo và cấu hình DispatcherTimer
            TTS_DiemDanhVaChamCongViewModel viewModel = new TTS_DiemDanhVaChamCongViewModel();
            viewModel.UserId = UserId.ToUpper();
            DataContext = viewModel;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;

            Loaded += (sender, e) => timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimeOffset localNow = DateTimeOffset.Now;

            // Hiển thị thời gian lên màn hình
            timeTextBlock.Text = localNow.ToString("yyyy-MM-dd HH:mm:ss");
            string timeZoneId = TimeZoneInfo.Local.DisplayName;

            // Hiển thị múi giờ lên màn hình
            timeZoneTextBlock.Text = "(" + timeZoneId + ")";
        }
    }
}
