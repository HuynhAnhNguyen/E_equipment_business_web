using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class ChiTietHd
{
    public string MaChiTietHd { get; set; } = null!;

    public string? MaHoaDon { get; set; }

    public string? MaDonDat { get; set; }

    public virtual DonDat? MaDonDatNavigation { get; set; }

    public virtual HoaDon? MaHoaDonNavigation { get; set; }
}
