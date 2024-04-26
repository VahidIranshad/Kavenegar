using Kavenegar.Domain.Entity;

namespace Kavenegar.Application.Contracts.Base
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        User User { get; }

    }
}
