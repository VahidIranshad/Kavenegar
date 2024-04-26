using AutoMapper;
using FluentValidation;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Create
{
    public class BlogCreateCommandHandler : ICommandHandler<BlogCreateCommand, BlogDto>
    {
        private readonly IUnitOfWork<BLog> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidator<BlogCrudDto> _validator;
        private readonly IBlogRepository _blogRepository;

        public BlogCreateCommandHandler(IUnitOfWork<BLog> unitOfWork, 
            IMapper mapper, 
            ICurrentUserService currentUserService, 
            IValidator<BlogCrudDto> validator,
            IBlogRepository blogRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _validator = validator;
            _blogRepository = blogRepository;
        }


        public async Task<BlogDto> Handle(BlogCreateCommand request, CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(request.blogCrudDto);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors.ToList());
                }
            }


            var data = _mapper.Map<BLog>(request.blogCrudDto);

            data = await _blogRepository.Add(data);
            await _unitOfWork.SaveChangesAsync(_currentUserService);
            return _mapper.Map<BlogDto>(data);
        }
    }
}
