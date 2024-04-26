using AutoMapper;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Features.BlogFeatures.Command.Delete;
using Kavenegar.Application.Profiles;
using Moq;
using UnitTest.Mocks;

namespace UnitTest.Feature.Blog
{
    public class BlogDeleteCommandHanderTest
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly BlogDeleteCommandHander _handler;
        
        public BlogDeleteCommandHanderTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _mockCurrentUserService = MockCurrentService.GetMock();
            _mockBlogRepository = MockBlogRepository.GetRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _handler = new BlogDeleteCommandHander(_mockUow.Object,_mockCurrentUserService.Object, _mockBlogRepository.Object);
        }


        [Fact]
        public async Task Valid_LeaveType_Deleted()
        {
            var result = await _handler.Handle(new BlogDeleteCommand() { Id = 1 }, CancellationToken.None);
            var blogs = await _mockBlogRepository.Object.GetAll();
            Assert.Equal(blogs.Count, 2);
        }
    }
}
