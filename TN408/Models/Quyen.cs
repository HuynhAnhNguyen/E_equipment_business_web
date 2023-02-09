using System;
using System.Collections.Generic;

namespace TN408.Models;

public partial class Quyen
{
    public string MaQuyen { get; set; } = null!;

    public string TenQuyen { get; set; } = null!;

    public virtual ICollection<Nhom> MaNhoms { get; } = new List<Nhom>();
}
