using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models.UserModelExtensions;

namespace WebApi.Validators
{
    //public class UserSaveValidator : AbstractValidator<UserSaveModel>
    //{
    //    /// <summary>  
    //    /// Validator rules for User Save Model
    //    /// </summary>  
    //    public UserSaveValidator()
    //    {
    //        RuleFor(x => x.Username)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.USERNAME_REQUIRED);

    //        RuleFor(x => x.Password)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.PASSWORD_REQUIRED);

    //        RuleFor(x => x.RoleId)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.ROLE_REQUIRED);

    //        RuleFor(x => x.UserTypeId)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.USER_TYPE_REQUIRED);

    //        RuleFor(x => x.UserFirstName)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.USER_FIRSTNAME_REQUIRED);

    //        RuleFor(x => x.UserLastName)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.USER_LASTNAME_REQUIRED);

    //        RuleFor(x => x.UserEmail)
    //            .NotEmpty()
    //            .WithMessage(UserValidationMessage.USER_EMAIL_REQUIRED);
    //    }
    //}
}
