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
    public class F2_TTS_NoiDungVideoKhoaHocViewModel : BaseViewModel
    {
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

        private string _Video;

        public string Video
        {
            get { return _Video; }
            set
            {
                _Video = value;
                OnPropertyChanged();
            }
        }

        private string _NoiDung;

        public string NoiDung
        {
            get { return _NoiDung; }
            set
            {
                _NoiDung = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadedCommand { get; set; }

        public F2_TTS_NoiDungVideoKhoaHocViewModel()
        {
            LoadedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                NoiDung = DataProvider.Ins.DB.tblNoiDungDaoTaos.Where(x => x.MaKhoaDaoTao == MaKhoaDaoTao).Select(x => x.NoiDung).SingleOrDefault();

            });
        }


    }
}
