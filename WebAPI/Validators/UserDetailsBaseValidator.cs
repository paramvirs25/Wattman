using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models;

namespace WebApi.Validators
{
    public class UserDetailsBaseValidator : AbstractValidator<UserDetailsBaseModel>
    {
        /// <summary>  
        /// Validator rules for User Save Model
        /// </summary>  
        public UserDetailsBaseValidator()
        {
            RuleFor(x => x.UserFirstName)
                .NotEmpty()
                .WithMessage(UserValidationMessage.USER_FIRSTNAME_REQUIRED);

            RuleFor(x => x.UserLastName)
                .NotEmpty()
                .WithMessage(UserValidationMessage.USER_LASTNAME_REQUIRED);

            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage(UserValidationMessage.USER_EMAIL_REQUIRED);
        }
    }
}
