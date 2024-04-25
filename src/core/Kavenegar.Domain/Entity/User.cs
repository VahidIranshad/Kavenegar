using Kavenegar.Domain.Base;

namespace Kavenegar.Domain.Entity
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
    }
}
