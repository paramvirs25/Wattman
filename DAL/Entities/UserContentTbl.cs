using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class UserContentTbl
    {
        public int UserId { get; set; }
        public int ContentId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }

        public ContentTbl Content { get; set; }
        public UserDetailsTbl User { get; set; }
    }
}
