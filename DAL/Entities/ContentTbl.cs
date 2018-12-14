using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class ContentTbl
    {
        public ContentTbl()
        {
            UserContentTbl = new HashSet<UserContentTbl>();
        }

        public int ContentId { get; set; }
        public string ContentUrl { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

        public UserDetailsTbl CreatedByNavigation { get; set; }
        public UserDetailsTbl ModifiedByNavigation { get; set; }
        public ICollection<UserContentTbl> UserContentTbl { get; set; }
    }
}
