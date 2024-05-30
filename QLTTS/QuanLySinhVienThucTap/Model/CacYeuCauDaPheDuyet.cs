using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class CacYeuCauDaPheDuyet
    {
        public int STT { get; set; }
        public int MaYeuCau { get; set; }
        public string TenTTS { get; set; }
        public string MaTTS { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayHieuLuc { get; set; }
        public string YeuCau { get; set; }
        public string Status { get; set; }
    }
}
