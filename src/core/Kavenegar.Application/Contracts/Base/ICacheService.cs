using Kavenegar.Domain.Base;

namespace Kavenegar.Application.Contracts.Base
{
    public interface ICacheService
    {
        Task<T> GetData<T>(int key);
        Task<bool> SetData<T>(T value) where T : BaseEntity;
        Task<bool> RemoveData<T>(int key);
    }
}
