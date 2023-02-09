using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class DonViTinh
{
    public string MaDvt { get; set; } = null!;

    public string TenDvt { get; set; } = null!;

    public virtual ICollection<SanPham> SanPhams { get; } = new List<SanPham>();
}
