using AutoMapper;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetByID
{
    public class GetBlogByIDQueryHandler : IQueryHandler<GetBlogByIDQuery, BlogDto>
    {

        public readonly IUnitOfWork<BLog> _unitOfWork;
        public readonly IMapper _mapper;


        public GetBlogByIDQueryHandler(IUnitOfWork<BLog> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BlogDto> Handle(GetBlogByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository().Get(request.Id);
            return _mapper.Map<BlogDto>(result);
        }

    }
}
