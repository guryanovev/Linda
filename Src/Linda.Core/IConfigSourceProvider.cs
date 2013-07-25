namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IConfigSourceProvider
    {
        List<ConfigGroup> GetCs(string path);
    }
}
