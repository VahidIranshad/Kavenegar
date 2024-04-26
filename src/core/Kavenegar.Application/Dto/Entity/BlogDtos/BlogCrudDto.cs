using FluentValidation;
using System.ComponentModel;

namespace Kavenegar.Application.Dto.Entity.BlogDtos
{
    public class BlogCrudDto
    {
        public int Id { get; set; }
        [DefaultValue(Domain.Const.UserConst.Admin_Id)]
        public  int AuthorId { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class BlogCrudDtoValidator : AbstractValidator<BlogCrudDto>
    {
        public BlogCrudDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(800).WithMessage("{PropertyName} must not exceed 800 characters.");
            RuleFor(p => p.Content)
                .MaximumLength(4000).WithMessage("{PropertyName} must not exceed 4000 characters.");

        }
    }
}
