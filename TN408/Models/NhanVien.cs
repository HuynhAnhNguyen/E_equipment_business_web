using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class NhanVien
{
    public string MaNhanVien { get; set; } = null!;

    public string HoNhanVien { get; set; } = null!;

    public string TenNhanVien { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? MaNhom { get; set; }

    public virtual Nhom? MaNhomNavigation { get; set; }
}
