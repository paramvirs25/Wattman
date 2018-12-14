using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models.UserModelExtensions;

namespace WebApi.Validators
{
    public class UserAuthenticateValidator : AbstractValidator<UserAuthenticateModel>
    {
        /// <summary>  
        /// Validator rules for User Authenticate Model
        /// </summary>  
        public UserAuthenticateValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserValidationMessage.PASSWORD_REQUIRED);
            //.Length(0, 100)
            //.WithMessage("The Product Name cannot be more than 100 characters.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(UserValidationMessage.USERNAME_REQUIRED);

            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("The Product ID must be at greather than 0.");



            //RuleFor(x => x.Description)
            //    .NotEmpty()
            //    .WithMessage("The Product Description must be at least 150 characters long.");

            //RuleFor(x => x.Price).GreaterThan(0).WithMessage("The Product Price must be at greather than 0.");
        }
    }
}
