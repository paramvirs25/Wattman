using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models;

namespace WebApi.Validators
{
    public class UserDetailsBaseAdminValidator : AbstractValidator<UserDetailsBaseAdminModel>
    {
        /// <summary>  
        /// Validator rules for User Save Model
        /// </summary>  
        public UserDetailsBaseAdminValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage(RoleValidationMessage.ROLE_ID_REQUIRED);

            RuleFor(x => x.UserTypeId)
                .NotEmpty()
                .WithMessage(UserTypeValidationMessage.USER_TYPE_REQUIRED);
        }
    }
}
