using Kavenegar.Application.Contracts.Base;
using Moq;

namespace UnitTest.Mocks
{
    internal class MockCurrentService
    {
        public static Mock<ICurrentUserService> GetMock()
        {

            var moc = new Mock<ICurrentUserService>();
            moc.Setup(r => r.UserId).Returns(Kavenegar.Domain.Const.UserConst.Admin_Id);
            moc.Setup(r => r.User).Returns(
                new Kavenegar.Domain.Entity.User
                {
                    Id = Kavenegar.Domain.Const.UserConst.Admin_Id,
                    Name = Kavenegar.Domain.Const.UserConst.Admin_Name
                });
            return moc;
        }
    }
}
