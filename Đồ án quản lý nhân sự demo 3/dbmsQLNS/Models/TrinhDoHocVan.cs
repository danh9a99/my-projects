//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dbmsQLNS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrinhDoHocVan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrinhDoHocVan()
        {
            this.NhanViens = new HashSet<NhanVien>();
        }
    
        public string MaTrinhDoHocVan { get; set; }
        public string TenTrinhDo { get; set; }
        public Nullable<double> HeSoBac { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
