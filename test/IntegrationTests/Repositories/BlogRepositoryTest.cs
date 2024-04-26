using AutoMapper;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Entity;
using Kavenegar.Infrastructure.Repositories.Common;
using Kavenegar.Infrastructure.Repositories.Entity;

namespace IntegrationTests.Repositories
{
    public class BlogRepositoryTest : IClassFixture<SharedDatabaseFixture>
    {
        private SharedDatabaseFixture Fixture { get; }
        private readonly IMapper _mapper;
        private ICurrentUserService _currentUser;
        public BlogRepositoryTest(SharedDatabaseFixture fixture)
        {
            Fixture = fixture;
            _mapper = SharedDatabaseFixture.mapper;
            _currentUser = SharedDatabaseSetup.Common.DefaultCurrentUserService.adminCurrentUserService.Object;

        }
        [Fact]
        public async Task Valid_GetData_ReturnsAllBook()
        {
            using (var context = Fixture.CreateContext())
            {
                var _unitOfWork = new UnitOfWork(context);
                var repository = new BlogRepository(context);

                var list = await repository.GetAll();

                Assert.Equal(4, list.Count);
            }
        }
        [Fact]
        public async Task Valid_GetData_ByID()
        {
            int id = 1;
            using (var context = Fixture.CreateContext())
            {
                var _unitOfWork = new UnitOfWork(context);
                var repository = new BlogRepository(context);

                var data = await repository.Get(id);

                Assert.Equal(data.Id, id);
                /*other properties*/
            }
        }

        [Fact]
        public async Task Valid_GetData_Insert()
        {
            using (var context = Fixture.CreateContext())
            {
                var _unitOfWork = new UnitOfWork(context);
                var repository = new BlogRepository(context);
                using (context.Database.BeginTransaction())
                {
                    var newBlog = new BLog { Id = 5, Title = "TitleTest4", Content = "ContentTest4", AuthorId = Kavenegar.Domain.Const.UserConst.Admin_Id };
                    newBlog = await repository.Add(newBlog);
                    var x =  _unitOfWork.SaveChangesAsync(_currentUser).Result;

                    var data = await repository.Get(newBlog.Id);
                    Assert.Equal(data.Id, data.Id);
                    Assert.Equal(data.Title, data.Title);
                    Assert.Equal(data.Content, data.Content);
                    await _unitOfWork.Rollback();
                }

            }
        }
    }
}
