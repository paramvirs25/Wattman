using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class UserTypesTbl
    {
        public UserTypesTbl()
        {
            UserDetailsTbl = new HashSet<UserDetailsTbl>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeDisplayName { get; set; }
        public string UserTypeName { get; set; }

        public ICollection<UserDetailsTbl> UserDetailsTbl { get; set; }
    }
}
