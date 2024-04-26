using AutoMapper;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Kavenegar.Application.Features.BlogFeatures.Command.Create;
using Kavenegar.Application.Profiles;
using Moq;
using UnitTest.Mocks;

namespace UnitTest.Feature.Blog
{
    public class BlogCreateCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<IBlogRepository> _mockBlogRepository;
        private readonly BlogCrudDto _crudDto;
        private readonly BlogCreateCommandHandler _handler;
        private readonly BlogCrudDtoValidator _Validator;
        public BlogCreateCommandHandlerTest()
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
            _handler = new BlogCreateCommandHandler(_mockUow.Object, _mapper, _mockCurrentUserService.Object, _Validator, _mockBlogRepository.Object);

            _crudDto = new BlogCrudDto
            {
                Title = "Test",
                Content = "Test DTO",
                AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id
            };

        }


        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new BlogCreateCommand() { blogCrudDto = _crudDto }, CancellationToken.None);
            var blogs = await _mockBlogRepository.Object.GetAll();
            Assert.Equal(blogs.Count, 4);
        }
    }
}
