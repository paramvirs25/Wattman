using System;

namespace WebApi.Models
{
    public class UserContentListModel
    {
        public int UserId { get; set; }
        public int ContentId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }

        public string ContentUrl { get; set; }
        public string ContentName { get; set; }
        public string ContentType { get; set; }
    }
}