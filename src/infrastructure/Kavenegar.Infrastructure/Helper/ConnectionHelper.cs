using Kavenegar.Domain.Base;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Kavenegar.Infrastructure.Helper
{
    public class ConnectionHelper
    {
        private readonly AppSettings _appSettings;
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public ConnectionHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_appSettings.RedisURL);
            });
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
