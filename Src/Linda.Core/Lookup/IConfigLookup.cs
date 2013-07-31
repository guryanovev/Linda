namespace Linda.Core.Lookup
{
    using System.Collections.Generic;

    public interface IConfigLookup
    {
        IEnumerable<ConfigGroup> GetConfigGroups(string path);
    }
}