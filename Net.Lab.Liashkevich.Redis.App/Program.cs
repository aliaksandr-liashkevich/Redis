using Net.Lab.Liashkevich.Redis.Caching;
using Net.Lab.Liashkevich.Redis.Caching.Services;
using System;

namespace Net.Lab.Liashkevich.Redis.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = "person";
            using (var redis = new RedisCacheProvider(new DataConverter(), "localhost"))
            {
                var person = new Person
                {
                    Name = "Alex",
                    Age = 11
                };

                redis.Set(key, person);

                person = redis.Get<Person>(key);

                Console.WriteLine(person.Name);
                Console.WriteLine(person.Age);
            }

            Console.ReadKey();
        }
    }

    [RedisCache(100)]
    class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
