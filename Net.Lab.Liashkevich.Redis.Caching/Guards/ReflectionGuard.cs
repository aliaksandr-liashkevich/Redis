using System;
using System.Linq;

namespace Net.Lab.Liashkevich.Redis.Caching.Guards
{
    public static class ReflectionGuard
    {
        public static TimeSpan GetDuration<T>(T data)
        {
            var attribute = GetRedisCacheAttribute(data.GetType());
            if (attribute == null)
            {
                throw new InvalidOperationException($"The {nameof(data)} isn't caching.");
            }

            return attribute.Duration;
        }

        public static RedisCacheAttribute GetRedisCacheAttribute(Type type)
        {
            ValidationGuard.CheckNull(type, nameof(type));
            var attributes = type.GetCustomAttributes(typeof(RedisCacheAttribute), false);

            return attributes.FirstOrDefault() as RedisCacheAttribute;
        }
    }
}
