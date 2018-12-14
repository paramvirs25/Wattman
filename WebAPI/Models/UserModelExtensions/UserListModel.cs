namespace WebApi.Models.UserModelExtensions
{
    public class UserListModel : UserDetailsModel
    {
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public string RoleName { get; set; }
        public string UserTypeName { get; set; }
        public bool IsAllContentWatched { get; set; }
    }
}