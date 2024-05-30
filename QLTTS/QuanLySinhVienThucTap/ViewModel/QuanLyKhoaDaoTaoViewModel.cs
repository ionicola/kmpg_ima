using QuanLySinhVienThucTap.Model;
using QuanLySinhVienThucTap.Pages.QuanLyKhoaDaoTaoPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace QuanLySinhVienThucTap.ViewModel
{
    public class QuanLyKhoaDaoTaoViewModel : BaseViewModel
    {
        public ICommand LoadKhoaDaoTaoCommand { get; set; }

        public ICommand PheDuyetCommand { get; set; }
        private ObservableCollection<CacKhoaDaoTao> _ListKhoaDaoTao;
        public ObservableCollection<CacKhoaDaoTao> ListKhoaDaoTao
        {
            get { return _ListKhoaDaoTao; }
            set
            {
                _ListKhoaDaoTao = value;
                OnPropertyChanged(nameof(ListKhoaDaoTao));
            }
        }

        private CacKhoaDaoTao _selectedKhoaDaoTao;

        public CacKhoaDaoTao SelectedKhoaDaoTao
        {
            get { return _selectedKhoaDaoTao; }
            set
            {
                _selectedKhoaDaoTao = value;
                OnPropertyChanged(nameof(SelectedKhoaDaoTao));
            }
        }

        private CacNhiemVuDaoTao _selectedNhiemVuPheDuyet;

        public CacNhiemVuDaoTao SelectedNhiemVuPheDuyet
        {
            get { return _selectedNhiemVuPheDuyet; }
            set
            {
                _selectedNhiemVuPheDuyet = value;
                OnPropertyChanged(nameof(SelectedNhiemVuPheDuyet));
                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn phê duyệt Nhiệm vụ khóa học này. Điều này không thể hoàn tác.", "Cảnh báo", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    PheDuyetNhiemVu();
                    LoadKhoaDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
                }
                else
                {
                    return;
                }

            }
        }

        private CacNhiemVuDaoTao _selectedNhiemVuDaoTao;

        public CacNhiemVuDaoTao SelectedNhiemVuDaoTao
        {
            get { return _selectedNhiemVuDaoTao; }
            set
            {
                _selectedNhiemVuDaoTao = value;
                OnPropertyChanged(nameof(SelectedNhiemVuDaoTao));
            }
        }

        private string _tenKhoaDaoTao;

        public string TenKhoaDaoTao
        {
            get { return _tenKhoaDaoTao; }
            set
            {
                _tenKhoaDaoTao = value;
                OnPropertyChanged(nameof(TenKhoaDaoTao));
            }
        }

        private string _userDepart;

        public string UserDepart
        {
            get { return _userDepart; }
            set
            {
                _userDepart = value;
                OnPropertyChanged(nameof(UserDepart));
            }
        }

        private ObservableCollection<CacNhiemVuDaoTao> _ListNhiemVuDaoTao;
        public ObservableCollection<CacNhiemVuDaoTao> ListNhiemVuDaoTao
        {
            get { return _ListNhiemVuDaoTao; }
            set
            {
                _ListNhiemVuDaoTao = value;
                OnPropertyChanged(nameof(ListNhiemVuDaoTao));
            }
        }
        public ICommand ThemKhoaHoc { get; set; }
        public ICommand XoaKhoaHoc { get; set; }
        public ICommand ThemNhiemVuKhoaHoc { get; set; }
        public ICommand SuaNhiemVuKhoaHoc { get; set; }
        public ICommand XoaNhiemVuKhoaHoc { get; set; }

        public QuanLyKhoaDaoTaoViewModel()
        {

            LoadKhoaDaoTaoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedKhoaDaoTao != null)
                {
                    TenKhoaDaoTao = SelectedKhoaDaoTao.TenKhoa;
                    LoadKhoaDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
                }

            });

            ThemKhoaHoc = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemKhoaDaoTao newKhoaDaoTao = new ThemKhoaDaoTao(UserDepart);
                newKhoaDaoTao.ShowDialog();
                OnLoaded();
            });

            XoaKhoaHoc = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XoaKhoaDaoTao();
                OnLoaded();

            });

            ThemNhiemVuKhoaHoc = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedKhoaDaoTao == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Khóa đào tạo cần thêm Nhiệm vụ.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ThemNhiemVu();
                LoadKhoaDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
            });

            SuaNhiemVuKhoaHoc = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedKhoaDaoTao == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Khóa đào tạo cần sửa Nhiệm vụ.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedNhiemVuDaoTao == null)
                {
                    MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ đào tạo cần sửa đổi.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedNhiemVuDaoTao.TienDo == "approved")
                {
                    MessageBox.Show("Có lỗi xảy ra. Không thể chỉnh sửa nhiệm vụ đã được xác nhận.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                SuaNhiemVu();
                LoadKhoaDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
            });

            XoaNhiemVuKhoaHoc = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XoaNhiemVu();
                LoadKhoaDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
            });
        }

        public void OnLoaded()
        {
            ListKhoaDaoTao = new ObservableCollection<CacKhoaDaoTao>();
            var listKhoaDaoTao = DataProvider.Ins.DB.tblKhoaDaoTaos.ToList();
            foreach (var item in listKhoaDaoTao)
            {
                CacKhoaDaoTao cackhoadaotao = new CacKhoaDaoTao();
                cackhoadaotao.MaKhoaDaoTao = item.MaKhoaDaoTao;
                cackhoadaotao.TenKhoa = item.TenKhoa;
                ListKhoaDaoTao.Add(cackhoadaotao);
            }
        }


        public void LoadKhoaDaoTao(string UserDepart, string MaKhoaDaoTao)
        {
            ListNhiemVuDaoTao = new ObservableCollection<CacNhiemVuDaoTao>();
            var maTTSList = DataProvider.Ins.DB.tblTTS
                .Where(p => p.MaPhongBan == UserDepart)
                .Select(p => p.MaTTS)
                .ToList();

            var nhiemVuList = DataProvider.Ins.DB.tblNhiemVuDaoTaos
                .Where(nv => maTTSList.Contains(nv.MaTTS) && nv.MaKhoaDaoTao == MaKhoaDaoTao)
                .ToList();
            int i = 1;
            foreach (var item in nhiemVuList)
            {
                CacNhiemVuDaoTao cacNhiemVuDaoTao = new CacNhiemVuDaoTao();

                cacNhiemVuDaoTao.STT = i;
                cacNhiemVuDaoTao.MaNhiemVuDaoTao = item.MaNhiemVuDaoTao;
                cacNhiemVuDaoTao.MaTTS = item.MaTTS;
                cacNhiemVuDaoTao.TenTTS = DataProvider.Ins.DB.tblTTS
                                        .Where(tts => tts.MaTTS == item.MaTTS)
                                        .Select(tts => tts.TenTTS)
                                        .FirstOrDefault();
                cacNhiemVuDaoTao.Deadline = item.Deadline;
                cacNhiemVuDaoTao.TienDo = item.status;
                ListNhiemVuDaoTao.Add(cacNhiemVuDaoTao);
                i++;
            }
        }

        public void PheDuyetNhiemVu()
        {
            tblNhiemVuDaoTao selectedNhiemVu = DataProvider.Ins.DB.tblNhiemVuDaoTaos
                                                .FirstOrDefault(p => p.MaNhiemVuDaoTao == SelectedNhiemVuPheDuyet.MaNhiemVuDaoTao);
            if (selectedNhiemVu != null)
            {
                selectedNhiemVu.status = "approved";
                DataProvider.Ins.DB.SaveChanges();
            }
        }

        public void XoaKhoaDaoTao()
        {
            if (SelectedKhoaDaoTao == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Khóa đào tạo.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khóa đào tạo này?\nĐiều này cũng sẽ xóa toàn bộ thông tin khóa đào tạo trên toàn hệ thống. Hãy cân nhắc.", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                    var khoaDaoTaoToRemove = DataProvider.Ins.DB.tblKhoaDaoTaos
               .FirstOrDefault(p => p.MaKhoaDaoTao == SelectedKhoaDaoTao.MaKhoaDaoTao);

                    if (khoaDaoTaoToRemove != null)
                    {
                        DataProvider.Ins.DB.tblKhoaDaoTaos.Remove(khoaDaoTaoToRemove);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Xóa Khóa đào tạo thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra. Vui lòng kiểm tra lại.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
            }
            else
            {
                return;
            }

        }

        public void ThemNhiemVu()
        {
            ThemNhiemVuDaoTao newNhiemVu = new ThemNhiemVuDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao);
            newNhiemVu.ShowDialog();
        }

        public void SuaNhiemVu()
        {
            SuaNhiemVuDaoTao newSuaNhiemVu = new SuaNhiemVuDaoTao(UserDepart, SelectedKhoaDaoTao.MaKhoaDaoTao, SelectedNhiemVuDaoTao);
            newSuaNhiemVu.ShowDialog();

        }

        public void XoaNhiemVu()
        {
            if (SelectedKhoaDaoTao == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Khóa đào tạo.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if (SelectedNhiemVuDaoTao == null)
            {
                MessageBox.Show("Có lỗi xảy ra! Vui lòng chọn Nhiệm vụ đào tạo.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SelectedNhiemVuDaoTao.TienDo != "done" && SelectedNhiemVuDaoTao.TienDo != "approved")
            {
                MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa nhiệm vụ đào tạo này?", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var nhiemVuToRemove = DataProvider.Ins.DB.tblNhiemVuDaoTaos
               .FirstOrDefault(p => p.MaNhiemVuDaoTao == SelectedNhiemVuDaoTao.MaNhiemVuDaoTao);

                    if (nhiemVuToRemove != null)
                    {
                        DataProvider.Ins.DB.tblNhiemVuDaoTaos.Remove(nhiemVuToRemove);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Xóa Nhiệm vụ đào tạo thành công!", "Thành công!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra. Vui lòng kiểm tra lại.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Không thể xóa do nhiệm vụ đã được hoàn thành hoặc được phê duyệt.", "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
