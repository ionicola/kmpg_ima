using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class CacNhiemVuDaoTao
    {
        public int STT { get; set; }
        public int MaNhiemVuDaoTao { get; set; }
        public string TenTTS { get; set; }
        public string MaTTS { get; set;}
        public DateTime? Deadline { get; set; }
        public string TienDo { get; set;}
    }
}
