using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace Kavenegar.Application.Features.BlogFeatures.Query.GetList
{
    public class BlogGetListQuery : IQuery<IList<BlogDto>>
    {
    }
}
