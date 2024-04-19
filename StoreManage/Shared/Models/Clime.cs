using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Clime
    {
        public Clime()
        {
            RoleClimes = new HashSet<RoleClime>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<RoleClime> RoleClimes { get; set; }
    }
}
