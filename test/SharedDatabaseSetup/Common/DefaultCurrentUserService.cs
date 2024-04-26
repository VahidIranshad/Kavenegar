using Kavenegar.Application.Contracts.Base;
using Moq;

namespace SharedDatabaseSetup.Common
{
    public class DefaultCurrentUserService
    {
        public static Mock<ICurrentUserService> adminCurrentUserService
        {
            get
            {
                var x = new Mock<ICurrentUserService>();
                x.Setup(r => r.UserId).Returns(Kavenegar.Domain.Const.UserConst.Admin_Id);
                x.Setup(r => r.User).Returns(
                    new Kavenegar.Domain.Entity.User
                    {
                        Id = Kavenegar.Domain.Const.UserConst.Admin_Id,
                        Name = Kavenegar.Domain.Const.UserConst.Admin_Name
                    });
                return x;
            }
        }

    }
}
