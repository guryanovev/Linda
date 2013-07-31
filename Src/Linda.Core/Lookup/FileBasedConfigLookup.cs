namespace Linda.Core.Lookup
{
    using System.Collections.Generic;

    public class FileBasedConfigLookup : IConfigLookup
    {
        private readonly IFilesSystem _filesSystem;

        public FileBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public FileBasedConfigLookup(IFilesSystem filesSystem)
        {
            _filesSystem = filesSystem;
        }

        public IEnumerable<ConfigGroup> GetConfigGroups(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}