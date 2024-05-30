using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class NhiemVuCacDuAn
    {
        public string MaNhiemVuDA { get; set; }
        public string NhiemVu { get; set; }
        public string TenTTS { get; set; }
        public string ChucVu { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public string Status { get; set; }
    }
}
