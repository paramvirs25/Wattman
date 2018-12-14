namespace WebApi.AppConstants.ValidationMessages
{
    public class ContentValidationMessage
    {
        public const string CONTENT_URL_REQUIRED = "Content URL is required.";
        public const string CONTENT_NAME_REQUIRED = "Content name is required.";
        public const string CONTENT_TYPE_REQUIRED = "Content type is required.";

        public const string CONTENT_NOT_FOUND = "Content not found.";
    }

    public class UserContentValidationMessage
    {
        public const string USER_CONTENT_NOT_FOUND = "User Content not found.";
    }

    public class UserValidationMessage
    {
        public const string USERNAME_PASSWORD_INCORRECT = "Username or password is incorrect.";
        public const string USER_NON_AUTHORIZED_ADMIN_AREA = "User not authorized to access admin area.";
        
        public const string USERNAME_ALREADY_TAKEN = "Username {0} is already taken.";
        public const string USER_NOT_FOUND = "User not found.";

        //Required
        public const string USERNAME_PASSWORD_REQUIRED = "Username and Password are required.";
        public const string USER_FIRSTNAME_REQUIRED = "First name is required.";
        public const string USER_LASTNAME_REQUIRED = "Last name is required.";
        public const string USER_EMAIL_REQUIRED = "Email is required.";
        public const string USERNAME_REQUIRED = "Username is required.";
        public const string PASSWORD_REQUIRED = "Password is required.";
        public const string ROLE_REQUIRED = "Role is required.";
        public const string USER_ID_REQUIRED = "User ID is required.";

    }

    public class RoleValidationMessage
    {
        public const string ROLE_ID_REQUIRED = "Role ID is required.";
    }

    public class UserTypeValidationMessage
    {
        public const string USER_TYPE_REQUIRED = "User type is required.";
    }
}
