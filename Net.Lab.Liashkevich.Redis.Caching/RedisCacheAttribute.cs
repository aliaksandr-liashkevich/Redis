using System;

namespace Net.Lab.Liashkevich.Redis.Caching
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RedisCacheAttribute : Attribute
    {
        public RedisCacheAttribute(int seconds)
        {
            Duration = TimeSpan.FromSeconds(seconds);
        }

        public TimeSpan Duration
        {
            get;
        }
    }
}
