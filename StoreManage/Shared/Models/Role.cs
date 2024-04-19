using System;
using System.Collections.Generic;

namespace StoreManage.Shared.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleClimes = new HashSet<RoleClime>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<RoleClime> RoleClimes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
