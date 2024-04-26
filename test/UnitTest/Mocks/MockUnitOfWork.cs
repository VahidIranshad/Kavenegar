using Kavenegar.Application.Contracts.Base;
using Moq;

namespace UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();

            return mockUow;
        }
    }
}
