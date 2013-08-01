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
            var result = new List<ConfigGroup>();

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                if (_filesSystem.Exists(currentDirectory))
                {
                    var configGroup = new ConfigGroup();
                    foreach (var file in _filesSystem.GetFiles(currentDirectory, "*.yml"))
                    {
                        var currentFile = file;
                        configGroup.AddConfigSource(new ConfigSource(() => _filesSystem.GetFileContent(currentFile)));
                    }

                    result.Add(configGroup);
                }

                currentDirectory = _filesSystem.GetParentDirectory(currentDirectory);
            }

            result.Reverse();

            return result;
        }
    }
}