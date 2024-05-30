using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySinhVienThucTap.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider Ins { get { if (_ins == null) _ins = new DataProvider(); return _ins; } set { _ins = value; } }
        public TTSEntities1 DB { get; set; }

        private DataProvider()
        {
            DB = new TTSEntities1();
        }


    }
}
