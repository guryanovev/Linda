namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IConfigSourceProvider
    {
        IEnumerable<ConfigGroup> GetConfigGroups(string path);
    }
}