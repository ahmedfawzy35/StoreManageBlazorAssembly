using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class RoleClime
    {
        public int Id { get; set; }
        public int ClimeId { get; set; }
        public int RoleId { get; set; }

        public virtual Clime Clime { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
