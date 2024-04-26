using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Exceptions;
using Kavenegar.Domain.Entity;
using MediatR;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Delete
{
    public class BlogDeleteCommandHander : ICommandHandler<BlogDeleteCommand, Unit>
    {
        private readonly IUnitOfWork<BLog> _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBlogRepository _blogRepository;

        public BlogDeleteCommandHander(IUnitOfWork<BLog> unitOfWork, ICurrentUserService currentUserService, IBlogRepository blogRepository)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _blogRepository = blogRepository;
        }


        public async Task<Unit> Handle(BlogDeleteCommand request, CancellationToken cancellationToken)
        {
            var data = await _blogRepository.Find(request.Id);
            if (data == null)
            {
                throw new NotFoundException(nameof(BLog), request.Id);
            }
            await _blogRepository.Delete(data);
            await _unitOfWork.SaveChangesAsync(_currentUserService);
            return Unit.Value;
        }
    }
}