using Kavenegar.Application.Contracts.Base;
using Kavenegar.Domain.Entity;

namespace Kavenegar.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
            //If we implemented the JWT Toke, we could get user value in this way.
            // Now we just create a user base our data
            //var user = (User?)httpContextAccessor.HttpContext.Items["User"]; ;
            user = new User { Id = Domain.Const.UserConst.Admin_Id, Name = Domain.Const.UserConst.Admin_Name };
            UserId = Domain.Const.UserConst.Admin_Id;
        }

        public int UserId { get; }

        public User user { get; }

    }
}
