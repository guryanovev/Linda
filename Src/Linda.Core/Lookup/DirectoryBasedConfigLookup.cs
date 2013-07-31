namespace Linda.Core.Lookup
{
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryBasedConfigLookup : IConfigLookup
    {
        private readonly IFilesSystem _filesSystem;

        public DirectoryBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem)
        {
            _filesSystem = filesSystem;
        }

        public IEnumerable<ConfigGroup> GetConfigGroups(string path)
        {
            var newConfigGroups = new List<ConfigGroup>();

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                var configDirectoryPath = Path.Combine(currentDirectory, "config");
                if (_filesSystem.Exists(configDirectoryPath))
                {
                    var configGroup = new ConfigGroup();
                    foreach (var file in _filesSystem.GetFiles(configDirectoryPath))
                    {
                        var currentFile = file;
                        configGroup.AddConfigSource(new ConfigSource(() => _filesSystem.GetFileContent(currentFile)));
                    }

                    newConfigGroups.Add(configGroup);
                }

                currentDirectory = _filesSystem.GetParentDirectory(currentDirectory);
            }

            newConfigGroups.Reverse();

            return newConfigGroups;
        }
    }
}
