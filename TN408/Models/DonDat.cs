using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class DonDat
{
    public string MaDonDat { get; set; } = null!;

    public int SoLuongDat { get; set; }

    public int TrangThai { get; set; }

    public string? MaKhachHang { get; set; }

    public string? MaSanPham { get; set; }

    public virtual ICollection<ChiTietHd> ChiTietHds { get; } = new List<ChiTietHd>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
