namespace HelloWorld.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Xe")]
    public partial class Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xe()
        {
            SoDienThoais = new HashSet<SoDienThoai>();
        }

        [Key]
        public int Maxe { get; set; }

        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(15)]
        public string BienSo { get; set; }

        [StringLength(500)]
        public string Mota { get; set; }

        [StringLength(10)]
        public string SoDienThoai { get; set; }

        public int? ChuSoHuu { get; set; }

        public int? GioDi { get; set; }

        public int? GioVe { get; set; }

        [StringLength(350)]
        public string DiemDi { get; set; }

        [StringLength(350)]
        public string DiemVe { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoDienThoai> SoDienThoais { get; set; }
    }
}
