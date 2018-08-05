using Net.Lab.Liashkevich.Redis.Caching.Interfaces;
using Newtonsoft.Json;

namespace Net.Lab.Liashkevich.Redis.Caching.Services
{
    public class DataConverter : IDataConverter
    {
        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}
