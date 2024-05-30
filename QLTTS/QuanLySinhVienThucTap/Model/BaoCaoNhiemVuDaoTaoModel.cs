using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class BaoCaoNhiemVuDaoTaoModel
    {
        public int STT { get; set; }
        public string MaKhoaDaoTao { get; set; }
        public string TenTTS { get; set; }
        public string TenKhoa { get; set; }
        public string MaNhiemVuDaoTao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? Deadline { get; set; }
        public string status { get; set; }


    }
}
