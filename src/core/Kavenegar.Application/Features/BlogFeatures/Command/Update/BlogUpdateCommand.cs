using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Update
{
    public class BlogUpdateCommand : ICommand
    {
        public required BlogCrudDto blogCrudDto { get; set; }
    }
}
