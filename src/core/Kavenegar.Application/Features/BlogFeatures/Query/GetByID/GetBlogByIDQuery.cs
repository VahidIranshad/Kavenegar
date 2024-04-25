using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetByID
{
    public class GetBlogByIDQuery : IQuery<BlogDto>
    {
        public int Id { get; set; }
    }
}
