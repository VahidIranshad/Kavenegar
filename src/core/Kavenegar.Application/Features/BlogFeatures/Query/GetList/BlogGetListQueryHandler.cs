using AutoMapper;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetList
{
    public class BlogGetListQueryHandler : IQueryHandler<BlogGetListQuery, IList<BlogDto>>
    {

        public readonly IUnitOfWork<BLog> _unitOfWork;
        public readonly IMapper _mapper;


        public BlogGetListQueryHandler(IUnitOfWork<BLog> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<BlogDto>> Handle(BlogGetListQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository().GetAll();
            return _mapper.Map<IList<BlogDto>>(result);
        }

    }
}
