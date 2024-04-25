namespace Kavenegar.Application.Dto.Entity.BlogDtos
{
    public class BlogDto
    {
        public int Id { get; set; } 
        public required string Title { get; set; }
        public required string Content { get; set; }

        public int AuthorId { get; set; }
        public required string AuthorName { get; set; }
    }
}
