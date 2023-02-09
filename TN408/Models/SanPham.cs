using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class SanPham
{
    public string MaSanPham { get; set; } = null!;

    public string TenSanPham { get; set; } = null!;

    public string? MaDvt { get; set; }

    public string? MaNsx { get; set; }

    public string? MaLoaiSp { get; set; }

    public string TenManHinh { get; set; } = null!;

    public string DungLuongPin { get; set; } = null!;

    public string TenChip { get; set; } = null!;

    public string DungLuongRam { get; set; } = null!;

    public string TenDoHoa { get; set; } = null!;

    public string HeDieuHanh { get; set; } = null!;

    public string DungLuongRom { get; set; } = null!;

    public string? MaMau { get; set; }

    public string HinhAnh { get; set; } = null!;

    public long GiaBan { get; set; }

    public int SoLuongTon { get; set; }

    public virtual ICollection<DonDat> DonDats { get; } = new List<DonDat>();

    public virtual DonViTinh? MaDvtNavigation { get; set; }

    public virtual LoaiSanPham? MaLoaiSpNavigation { get; set; }

    public virtual MauSac? MaMauNavigation { get; set; }

    public virtual NhaSanXuat? MaNsxNavigation { get; set; }
}
