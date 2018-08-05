using System;

namespace Net.Lab.Liashkevich.Redis.Caching.Guards
{
    public static class ValidationGuard
    {
        public static void CheckNull<T>(T instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void CheckNullOrEmpty(string instance, string name)
        {
            if (string.IsNullOrEmpty(instance))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
