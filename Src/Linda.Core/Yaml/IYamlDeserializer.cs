namespace Linda.Core.Yaml
{
    public interface IYamlDeserializer
    {
        T Deserialize<T>(string content) where T : new();
    }
}