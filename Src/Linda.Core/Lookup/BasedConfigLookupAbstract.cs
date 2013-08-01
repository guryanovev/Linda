namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BasedConfigLookupAbstract : IConfigLookup
    {
        public abstract ConfigGroup GetConfigGroup(ref string directory);

        public IEnumerable<ConfigGroup> GetConfigGroups(string path)
        {
            var result = new List<ConfigGroup>();

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                result.Add(GetConfigGroup(ref currentDirectory));
            }

            result.Reverse();

            return result;
        }
    }
}
