namespace Linda.Core.Lookup
{
    using System.Collections.Generic;

    public class FileBasedConfigLookup : BasedConfigLookupAbstract, IConfigLookup
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
            var result = new List<ConfigGroup>();

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                result.Add(this.GetConfigGroup(ref currentDirectory));
            }

            result.Reverse();

            return result;
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            if (_filesSystem.Exists(directory))
            {
                foreach (var file in _filesSystem.GetFiles(directory, "*.yml"))
                {
                    var currentFile = file;
                    result.AddConfigSource(new ConfigSource(() => _filesSystem.GetFileContent(currentFile)));
                }
            }

            directory = _filesSystem.GetParentDirectory(directory);

            return result;
        }
    }
}