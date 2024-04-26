using AutoMapper;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Features.BlogFeatures.Command.Create;
using Kavenegar.Application.Features.BlogFeatures.Command.Update;
using Kavenegar.Application.Profiles;
using Moq;
using UnitTest.Mocks;


namespace UnitTest.Feature.Blog
{
    public class BlogUpdateCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly BlogCrudDto _crudDto;
        private readonly BlogUpdateCommandHandler _handler;
        private readonly BlogCrudDtoValidator _Validator;

        public BlogUpdateCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _mockCurrentUserService = MockCurrentService.GetMock();
            _mockBlogRepository = MockBlogRepository.GetRepository();
            _Validator = new BlogCrudDtoValidator();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new BlogUpdateCommandHandler(_mockUow.Object, _mapper, _mockCurrentUserService.Object, _Validator, _mockBlogRepository.Object);

            _crudDto = new BlogCrudDto
            {
                Id = 1,
                Title = "TestUpdated",
                Content = "TestUpdated DTO",
                AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id
            };

        }
        [Fact]
        public async Task Valid_LeaveType_Updated()
        {
            var result = await _handler.Handle(new BlogUpdateCommand() { blogCrudDto = _crudDto }, CancellationToken.None);
            var blog = await _mockBlogRepository.Object.Get(1);
            Assert.Equal(_crudDto.Title, blog.Title);
            Assert.Equal(_crudDto.Content, blog.Content);
        }
    }
}
