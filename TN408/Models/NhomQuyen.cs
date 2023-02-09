using System;
using System.Collections.Generic;

namespace TN408.Models
{
    public partial class NhomQuyen
    {
        public string? MaNhom { get; set; }
        public string? MaQuyen { get; set; }

        public virtual Nhom? MaNhomNavigation { get; set; }
        public virtual Quyen? MaQuyenNavigation { get; set; }
    }
}
