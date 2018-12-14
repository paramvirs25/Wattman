namespace WebApi.Models
{
    public class UserDetailsBaseAdminModel : UserDetailsBaseModel
    {
        public int RoleId { get; set; }
        public int UserTypeId { get; set; }        
    }
}