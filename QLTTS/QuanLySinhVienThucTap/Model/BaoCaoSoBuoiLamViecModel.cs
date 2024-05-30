using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class BaoCaoSoBuoiLamViecModel
    {
        public int STT { get; set; }
        public string MaTTS { get; set; }
        public string TenTTS { get; set; }
        public DateTime? NgayChamCong { get; set; }
        public TimeSpan? GioChamCong { get; set; }
    }
}
