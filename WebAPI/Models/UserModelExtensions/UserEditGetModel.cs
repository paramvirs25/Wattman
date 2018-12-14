using System.Collections.Generic;

namespace WebApi.Models.UserModelExtensions
{
    public class UserEditGetModel
    {
        public UserModel User { get; set; }
        public UserDetailsBaseAdminModel UserDetailsBaseAdmin { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
        public IEnumerable<UserTypesModel> UserTypes { get; set; }
    }
}