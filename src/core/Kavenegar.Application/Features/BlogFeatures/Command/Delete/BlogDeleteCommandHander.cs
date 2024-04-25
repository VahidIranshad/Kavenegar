using AutoMapper;
using FluentValidation;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;
using MediatR;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Delete
{
    public class BlogDeleteCommandHander : ICommandHandler<BlogDeleteCommand, Unit>
    {
        private readonly IUnitOfWork<BLog> _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public BlogDeleteCommandHander(IUnitOfWork<BLog> unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }


        public async Task<Unit> Handle(BlogDeleteCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository().Delete(request.Id);
            await _unitOfWork.SaveChangesAsync(_currentUserService);
            return Unit.Value;
        }
    }
}