using Kavenegar.Application.BuildingBlocks.CQRS;
using Kavenegar.Application.Dto.Entity.BlogDtos;

namespace Kavenegar.Application.Features.BlogFeatures.Command.Delete
{
    public class BlogDeleteCommand : ICommand
    {
        public int Id { get; set; }
    }
}
