using AutoMapper;
using FluentValidation;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Exceptions;
using Kavenegar.Domain.Entity;
using MediatR;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Update
{
    public class BlogUpdateCommandHandler : ICommandHandler<BlogUpdateCommand, Unit>
    {
        private readonly IUnitOfWork<BLog> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidator<BlogCrudDto> _validator;
        private readonly IBlogRepository _blogRepository;

        public BlogUpdateCommandHandler(
            IUnitOfWork<BLog> unitOfWork,
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


        public async Task<Unit> Handle(BlogUpdateCommand request, CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                var result = await _validator.ValidateAsync(request.blogCrudDto);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors.ToList());
                }
            }

            var data = await _blogRepository.Find(request.blogCrudDto.Id);
            if (data == null)
            {
                throw new NotFoundException(nameof(BLog), request.blogCrudDto.Id);
            }

            _mapper.Map(request.blogCrudDto, data);

            await _blogRepository.Update(data);
            await _unitOfWork.SaveChangesAsync(_currentUserService);
            return Unit.Value;
        }
    }
}
