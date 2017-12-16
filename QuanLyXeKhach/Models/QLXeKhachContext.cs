namespace HelloWorld.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLXeKhachContext : DbContext
    {
        public QLXeKhachContext()
            : base("name=QLXeKhachContext")
        {
        }

        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SoDienThoai> SoDienThoais { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Xes)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.ChuSoHuu);

            modelBuilder.Entity<SoDienThoai>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<SoDienThoai>()
                .HasMany(e => e.Xes)
                .WithMany(e => e.SoDienThoais)
                .Map(m => m.ToTable("XeSoDienThoai").MapLeftKey("MaDT").MapRightKey("Maxe"));

            modelBuilder.Entity<Xe>()
                .Property(e => e.BienSo)
                .IsUnicode(false);

            modelBuilder.Entity<Xe>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();
        }
    }
}
