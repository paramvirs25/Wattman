namespace WebApi.Models.UserModelExtensions
{
    public class UserAuthenticateModel : UserModel
    {
        public bool CheckAdminRole { get; set; }
    }
}