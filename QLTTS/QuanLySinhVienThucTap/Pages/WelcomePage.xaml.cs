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

namespace QuanLySinhVienThucTap.Pages
{
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
            string videoPath = "..\\..\\Static\\Videos\\KPMG OnDemand App Introduction.mp4";
            mediaElement.Source = new Uri(videoPath, UriKind.RelativeOrAbsolute);
            Loaded += WelcomePage_Loaded;
            mediaElement.MediaEnded += MediaElement_MediaEnded;
        }

        private void WelcomePage_Loaded(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (s, args) =>
            {
                mediaElement.Position = TimeSpan.Zero;
                mediaElement.Play();

                timer.Stop();
            };
            timer.Start();
        }
    }
    }
