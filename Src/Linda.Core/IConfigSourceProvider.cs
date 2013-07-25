namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IConfigSourceProvider
    {
        List<ConfigGroup> GetConfigGroups(string path);
    }
}
