using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class Nhom
{
    public string MaNhom { get; set; } = null!;

    public string TenNhom { get; set; } = null!;

    public virtual ICollection<KhachHang> KhachHangs { get; } = new List<KhachHang>();

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();

    public virtual ICollection<Quyen> MaQuyens { get; } = new List<Quyen>();
}
