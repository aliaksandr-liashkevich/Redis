using Net.Lab.Liashkevich.Redis.Caching.Guards;
using Net.Lab.Liashkevich.Redis.Caching.Interfaces;
using StackExchange.Redis;
using System;

namespace Net.Lab.Liashkevich.Redis.Caching.Services
{
    public class RedisCacheProvider : IRedisCacheProvider
    {
        private readonly IDataConverter _dataConverter;
        private readonly ConnectionMultiplexer _connection;

        private bool _disposed = false;

        public RedisCacheProvider(IDataConverter dataConverter,
            string configuration)
        {
            ValidationGuard.CheckNull(dataConverter, nameof(dataConverter));
            ValidationGuard.CheckNullOrEmpty(configuration, nameof(configuration));

            _dataConverter = dataConverter;
            _connection = ConnectionMultiplexer.Connect(configuration);
        }

        ~RedisCacheProvider()
        {
            Dispose(false);
        }

        public T Get<T>(string key)
        {
            var data = _RedisDb.HashGet(key, _dataConverter.Serialize(key));
            
            if (data.IsNull)
            {
                return default(T);
            }
            else
            {
                return _dataConverter.Deserialize<T>(data.ToString());
            }
        }

        public bool IsInCache(string key)
        {
            return _RedisDb.HashExists(key, _dataConverter.Serialize(key));
        }

        public bool Remove(string key)
        {
           return _RedisDb.HashDelete(key, _dataConverter.Serialize(key));
        }

        public bool Set<T>(string key, T data)
        {
            var duration = ReflectionGuard.GetDuration(data);
            var transaction = _RedisDb.CreateTransaction();
            transaction.HashSetAsync(key, _dataConverter.Serialize(key), _dataConverter.Serialize(data));
            transaction.KeyExpireAsync(key, duration);
            var execute = transaction.ExecuteAsync();
            _RedisDb.Wait(execute);

            return execute.Result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Dispose();
                }
            }

            _disposed = true;
        }

        private IDatabase _RedisDb
        {
            get
            {
                return _connection.GetDatabase();
            }
        }
    }
}
