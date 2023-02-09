using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TN408.Models;

public partial class TN408Context : DbContext
{
    public TN408Context()
    {
    }

    public TN408Context(DbContextOptions<TN408Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }

    public virtual DbSet<DonDat> DonDats { get; set; }

    public virtual DbSet<DonViTinh> DonViTinhs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<LoaiSanPham> LoaiSanPhams { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Nhom> Nhoms { get; set; }

    public virtual DbSet<Quyen> Quyens { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-VCL1NL6;Initial Catalog=TN408;TrustServerCertificate=True; Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaChiTietHd).HasName("PK__ChiTietH__651E49EB9D0B2D80");

            entity.ToTable("ChiTietHD");

            entity.Property(e => e.MaChiTietHd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaChiTietHD");
            entity.Property(e => e.MaDonDat)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaDonDatNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaDonDat)
                .HasConstraintName("FK__ChiTietHD__MaDon__5CD6CB2B");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__ChiTietHD__MaHoa__5BE2A6F2");
        });

        modelBuilder.Entity<DonDat>(entity =>
        {
            entity.HasKey(e => e.MaDonDat).HasName("PK__DonDat__CD361BAC19144A48");

            entity.ToTable("DonDat");

            entity.Property(e => e.MaDonDat)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonDats)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__DonDat__MaKhachH__5812160E");

            entity.HasOne(d => d.MaSanPhamNavigation).WithMany(p => p.DonDats)
                .HasForeignKey(d => d.MaSanPham)
                .HasConstraintName("FK__DonDat__MaSanPha__59063A47");
        });

        modelBuilder.Entity<DonViTinh>(entity =>
        {
            entity.HasKey(e => e.MaDvt).HasName("PK__DonViTin__3D895AFE41C77499");

            entity.ToTable("DonViTinh");

            entity.Property(e => e.MaDvt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaDVT");
            entity.Property(e => e.TenDvt)
                .HasMaxLength(40)
                .HasColumnName("TenDVT");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__HoaDon__835ED13B88C4B724");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHoaDon)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaKm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaKM");
            entity.Property(e => e.NgayXuatHd)
                .HasColumnType("date")
                .HasColumnName("NgayXuatHD");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__HoaDon__MaKhachH__4E88ABD4");

            entity.HasOne(d => d.MaKmNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKm)
                .HasConstraintName("FK__HoaDon__MaKM__4F7CD00D");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__KhachHan__88D2F0E559ABEC18");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(3);
            entity.Property(e => e.HoKhachHang).HasMaxLength(30);
            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(50);

            entity.HasOne(d => d.MaNhomNavigation).WithMany(p => p.KhachHangs)
                .HasForeignKey(d => d.MaNhom)
                .HasConstraintName("FK__KhachHang__MaNho__4BAC3F29");
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaKm).HasName("PK__KhuyenMa__2725CF15B478928A");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.MaKm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaKM");
            entity.Property(e => e.GiaTriKm).HasColumnName("GiaTriKM");
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(200);
        });

        modelBuilder.Entity<LoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.MaLoaiSp).HasName("PK__LoaiSanP__1224CA7CC946C7DC");

            entity.ToTable("LoaiSanPham");

            entity.Property(e => e.MaLoaiSp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaLoaiSP");
            entity.Property(e => e.TenLoaiSp)
                .HasMaxLength(40)
                .HasColumnName("TenLoaiSP");
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.MaMau).HasName("PK__MauSac__3A5BBB7DDABF8E8A");

            entity.ToTable("MauSac");

            entity.Property(e => e.MaMau)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenMau).HasMaxLength(40);
        });

        modelBuilder.Entity<NhaSanXuat>(entity =>
        {
            entity.HasKey(e => e.MaNsx).HasName("PK__NhaSanXu__3A1BDBD2E14C1240");

            entity.ToTable("NhaSanXuat");

            entity.Property(e => e.MaNsx)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNSX");
            entity.Property(e => e.TenNsx)
                .HasMaxLength(40)
                .HasColumnName("TenNSX");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__NhanVien__77B2CA47F70A3016");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(3);
            entity.Property(e => e.HoNhanVien).HasMaxLength(30);
            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MatKhau)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenNhanVien).HasMaxLength(50);

            entity.HasOne(d => d.MaNhomNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaNhom)
                .HasConstraintName("FK__NhanVien__MaNhom__48CFD27E");
        });

        modelBuilder.Entity<Nhom>(entity =>
        {
            entity.HasKey(e => e.MaNhom).HasName("PK__Nhom__234F91CD5060F53C");

            entity.ToTable("Nhom");

            entity.Property(e => e.MaNhom)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenNhom).HasMaxLength(40);

            entity.HasMany(d => d.MaQuyens).WithMany(p => p.MaNhoms)
                .UsingEntity<Dictionary<string, object>>(
                    "NhomQuyen",
                    r => r.HasOne<Quyen>().WithMany()
                        .HasForeignKey("MaQuyen")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Nhom_Quye__MaQuy__45F365D3"),
                    l => l.HasOne<Nhom>().WithMany()
                        .HasForeignKey("MaNhom")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Nhom_Quye__MaNho__44FF419A"),
                    j =>
                    {
                        j.HasKey("MaNhom", "MaQuyen").HasName("PK_NQ");
                        j.ToTable("Nhom_Quyen");
                    });
        });

        modelBuilder.Entity<Quyen>(entity =>
        {
            entity.HasKey(e => e.MaQuyen).HasName("PK__Quyen__1D4B7ED48547425D");

            entity.ToTable("Quyen");

            entity.Property(e => e.MaQuyen)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenQuyen).HasMaxLength(40);
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSanPham).HasName("PK__SanPham__FAC7442D9F3DBD34");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSanPham)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DungLuongPin)
                .HasMaxLength(100)
                .HasColumnName("DungLuongPIN");
            entity.Property(e => e.DungLuongRam)
                .HasMaxLength(100)
                .HasColumnName("DungLuongRAM");
            entity.Property(e => e.DungLuongRom)
                .HasMaxLength(100)
                .HasColumnName("DungLuongROM");
            entity.Property(e => e.HeDieuHanh).HasMaxLength(100);
            entity.Property(e => e.HinhAnh).HasMaxLength(200);
            entity.Property(e => e.MaDvt)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaDVT");
            entity.Property(e => e.MaLoaiSp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaLoaiSP");
            entity.Property(e => e.MaMau)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaNsx)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaNSX");
            entity.Property(e => e.TenChip).HasMaxLength(100);
            entity.Property(e => e.TenDoHoa).HasMaxLength(100);
            entity.Property(e => e.TenManHinh).HasMaxLength(200);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.MaDvtNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDvt)
                .HasConstraintName("FK__SanPham__MaDVT__52593CB8");

            entity.HasOne(d => d.MaLoaiSpNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaLoaiSp)
                .HasConstraintName("FK__SanPham__MaLoaiS__5441852A");

            entity.HasOne(d => d.MaMauNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaMau)
                .HasConstraintName("FK__SanPham__MaMau__5535A963");

            entity.HasOne(d => d.MaNsxNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNsx)
                .HasConstraintName("FK__SanPham__MaNSX__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
