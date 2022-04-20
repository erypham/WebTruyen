using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace webtruyen.Models
{
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }

        public virtual DbSet<CHUONGTRUYEN> CHUONGTRUYENs { get; set; }
        public virtual DbSet<LOAITAIKHOAN> LOAITAIKHOANs { get; set; }
        public virtual DbSet<TACGIA> TACGIAs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<THELOAI> THELOAIs { get; set; }
        public virtual DbSet<TRUYEN> TRUYENs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LOAITAIKHOAN>()
                .HasMany(e => e.TAIKHOANs)
                .WithOptional(e => e.LOAITAIKHOAN)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.SDT_ND)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.OTB)
                .IsUnicode(false);

            modelBuilder.Entity<THELOAI>()
                .HasMany(e => e.TRUYENs)
                .WithOptional(e => e.THELOAI)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TRUYEN>()
                .Property(e => e.HINHANH)
                .IsUnicode(false);

            modelBuilder.Entity<TRUYEN>()
                .HasMany(e => e.CHUONGTRUYENs)
                .WithOptional(e => e.TRUYEN)
                .WillCascadeOnDelete();
        }
    }
}
