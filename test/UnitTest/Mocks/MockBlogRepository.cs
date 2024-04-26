using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Entity;
using Moq;

namespace UnitTest.Mocks
{
    internal static class MockBlogRepository
    {
        public static Mock<IBlogRepository> GetRepository()
        {
            var list = new List<BLog>
            {
                new BLog
                {
                    Id = 1,
                    Title = "Test1",
                    Content = "Test1"
                },
                new BLog
                {
                    Id = 2,
                    Title = "Test2",
                    Content = "Test2"
                },
                new BLog
                {
                    Id = 3,
                    Title = "Test3",
                    Content = "Test3"
                }
            };

            var mockRepo = new Mock<IBlogRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return list.Where( p => p.Id == id).First();
            });

            mockRepo.Setup(r => r.Find(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return list.Where(p => p.Id == id).First();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<BLog>())).ReturnsAsync((BLog blog) =>
            {
                list.Add(blog);
                return blog;
            });
            mockRepo.Setup(r => r.Update(It.IsAny<BLog>())).Callback((BLog blog) =>
            {
                var data = list.First(p => p.Id == blog.Id);
                data = blog;
            });
            mockRepo.Setup(r => r.Delete(It.IsAny<BLog>())).Callback((BLog blog) =>
            {
                list.Remove(blog);
            });

            return mockRepo;

        }
    }
}
