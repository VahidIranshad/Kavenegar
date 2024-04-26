using AutoMapper;
using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetByID
{
    public class GetBlogByIDQueryHandler : IQueryHandler<GetBlogByIDQuery, BlogDto>
    {

        public readonly IBlogRepository _blogRepository;
        public readonly IMapper _mapper;


        public GetBlogByIDQueryHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<BlogDto> Handle(GetBlogByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _blogRepository.Get(request.Id);
            return _mapper.Map<BlogDto>(result);
        }

    }
}
