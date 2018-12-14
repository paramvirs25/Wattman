using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class RolesTbl
    {
        public RolesTbl()
        {
            UserDetailsTbl = new HashSet<UserDetailsTbl>();
        }

        public int RoleId { get; set; }
        public string RoleDisplayName { get; set; }
        public string RoleName { get; set; }

        public ICollection<UserDetailsTbl> UserDetailsTbl { get; set; }
    }
}
