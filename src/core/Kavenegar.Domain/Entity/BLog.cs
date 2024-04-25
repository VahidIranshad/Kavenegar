using Kavenegar.Domain.Base;

namespace Kavenegar.Domain.Entity
{
    public class BLog : BaseEntity
    {
        public required string Title { get; set; }
        public required string Content { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
