using AutoMapper;
using FluentValidation;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
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

        public BlogUpdateCommandHandler(IUnitOfWork<BLog> unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IValidator<BlogCrudDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _validator = validator;
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

            var data = await _unitOfWork.Repository().Get(request.blogCrudDto.Id);
            if (data == null)
            {
                throw new NotFoundException(nameof(BLog), request.blogCrudDto.Id);
            }

            //var data = _mapper.Map<BLog>(request.blogCrudDto);
            _mapper.Map(request.blogCrudDto, data);

            await _unitOfWork.Repository().Update(data);
            await _unitOfWork.SaveChangesAsync(_currentUserService);
            return Unit.Value;
        }
    }
}
