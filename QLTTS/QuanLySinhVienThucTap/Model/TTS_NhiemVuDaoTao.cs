using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class TTS_NhiemVuDaoTao
    {
        public int STT { get; set; }
        public int MaNhiemVuDaoTao { get; set; }
        public string TenKhoa { get; set; }
        public string MaKhoaDaoTao { get; set; }
        public string Status { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
