using System;

namespace WebApi.Models
{
    public class UserContentModel
    {
        public int UserId { get; set; }
        public int ContentId { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}