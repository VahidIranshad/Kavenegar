using FluentValidation;

namespace Kavenegar.Application.Dto.Entity.BlogDtos
{
    public class BlogCrudDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class BlogDtoCrudValidator : AbstractValidator<BlogCrudDto>
    {
        public BlogDtoCrudValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(800).WithMessage("{PropertyName} must not exceed 800 characters.");
            RuleFor(p => p.Content)
                .MaximumLength(8000).WithMessage("{PropertyName} must not exceed 8000 characters.");

        }
    }
}
