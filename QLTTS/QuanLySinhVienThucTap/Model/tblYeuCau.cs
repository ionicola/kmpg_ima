//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLySinhVienThucTap.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblYeuCau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblYeuCau()
        {
            this.tblNguoiPheDuyets = new HashSet<tblNguoiPheDuyet>();
        }
    
        public int MaYeuCau { get; set; }
        public Nullable<System.DateTime> NgayNop { get; set; }
        public string MaTTS { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayHieuLuc { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNguoiPheDuyet> tblNguoiPheDuyets { get; set; }
        public virtual tblTT tblTT { get; set; }
    }
}