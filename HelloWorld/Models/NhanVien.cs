namespace HelloWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            Xes = new HashSet<Xe>();
        }

        [Key]
        public int MaNV { get; set; }

        [StringLength(150)]
        public string IdGG { get; set; }

        [StringLength(150)]
        public string ImageURL { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(150)]
        public string HoTen { get; set; }

        [StringLength(150)]
        public string TaiKhoan { get; set; }

        [StringLength(150)]
        public string MatKhau { get; set; }

        public short? VaiTro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
