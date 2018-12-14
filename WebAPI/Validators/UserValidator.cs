using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models;

namespace WebApi.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        /// <summary>  
        /// Validator rules for User Save Model
        /// </summary>  
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(UserValidationMessage.USERNAME_REQUIRED);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserValidationMessage.PASSWORD_REQUIRED);
        }
    }
}
