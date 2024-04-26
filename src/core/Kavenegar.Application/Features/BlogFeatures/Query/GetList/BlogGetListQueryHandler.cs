using AutoMapper;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetList
{
    public class BlogGetListQueryHandler : IQueryHandler<BlogGetListQuery, IList<BlogDto>>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;


        public BlogGetListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IBlogRepository blogRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task<IList<BlogDto>> Handle(BlogGetListQuery request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.GetAll();
            return _mapper.Map<IList<BlogDto>>(result);
        }

    }
}
