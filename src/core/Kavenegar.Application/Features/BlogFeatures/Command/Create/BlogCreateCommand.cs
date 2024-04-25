using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Create
{
    public class BlogCreateCommand : ICommand<BlogDto>
    {
        public required BlogCrudDto blogCrudDto { get; set; }
    }
}
