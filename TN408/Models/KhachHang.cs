using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string HoKhachHang { get; set; } = null!;

    public string TenKhachHang { get; set; } = null!;

    public DateTime NgaySinh { get; set; }

    public string GioiTinh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? MaNhom { get; set; }

    public virtual ICollection<DonDat> DonDats { get; } = new List<DonDat>();

    public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();

    public virtual Nhom? MaNhomNavigation { get; set; }
}
