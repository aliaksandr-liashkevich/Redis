namespace Net.Lab.Liashkevich.Redis.Caching.Interfaces
{
    public interface IDataConverter
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}
