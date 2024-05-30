using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class TTS_NhiemVuCacDuAn
    {
        public int STT { get; set; }
        public int MaNhiemVuDA { get; set; }
        public string NhiemVu { get; set; }
        public string MaTTS { get; set; }
        public string TenTTS { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public string Status { get; set; }
    }
}
