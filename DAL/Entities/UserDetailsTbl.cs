using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class UserDetailsTbl
    {
        public UserDetailsTbl()
        {
            ContentTblCreatedByNavigation = new HashSet<ContentTbl>();
            ContentTblModifiedByNavigation = new HashSet<ContentTbl>();
            InverseCreatedByNavigation = new HashSet<UserDetailsTbl>();
            InverseModifiedByNavigation = new HashSet<UserDetailsTbl>();
            UserContentTbl = new HashSet<UserContentTbl>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int UserTypeId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        public UserDetailsTbl CreatedByNavigation { get; set; }
        public UserDetailsTbl ModifiedByNavigation { get; set; }
        public RolesTbl Role { get; set; }
        public UsersTbl User { get; set; }
        public UserTypesTbl UserType { get; set; }
        public ICollection<ContentTbl> ContentTblCreatedByNavigation { get; set; }
        public ICollection<ContentTbl> ContentTblModifiedByNavigation { get; set; }
        public ICollection<UserDetailsTbl> InverseCreatedByNavigation { get; set; }
        public ICollection<UserDetailsTbl> InverseModifiedByNavigation { get; set; }
        public ICollection<UserContentTbl> UserContentTbl { get; set; }
    }
}
