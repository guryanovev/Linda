namespace Linda.Core.Yaml
{
    using System;

    public interface IYamlDeserializer
    {
        T Deserialize<T>(string content) where T : new();

        object Deserialize(Type type, string content);
    }
}