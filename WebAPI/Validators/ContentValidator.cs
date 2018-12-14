using FluentValidation;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Models;

namespace WebApi.Validators
{
    public class ContentValidator : AbstractValidator<ContentModel>
    {
        /// <summary>  
        /// Validator rules for User Save Model
        /// </summary>  
        public ContentValidator()
        {
            RuleFor(x => x.ContentUrl)
                .NotEmpty()
                .WithMessage(ContentValidationMessage.CONTENT_URL_REQUIRED);

            RuleFor(x => x.ContentName)
                .NotEmpty()
                .WithMessage(ContentValidationMessage.CONTENT_NAME_REQUIRED);

            RuleFor(x => x.ContentType)
                .NotEmpty()
                .WithMessage(ContentValidationMessage.CONTENT_TYPE_REQUIRED);
        }
    }
}
