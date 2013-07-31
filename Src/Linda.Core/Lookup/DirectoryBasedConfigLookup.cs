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

            // todo call file system methods

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                var configDirectoryPath = Path.Combine(currentDirectory, "config");
                if (_filesSystem.Exists(configDirectoryPath))
                {
                    // todo create config group
                    foreach (var file in _filesSystem.GetFiles(configDirectoryPath))
                    {
                        var currentFile = file;
                        // todo extract config source info
                        new ConfigSource(() => _filesSystem.GetFileContent(currentFile));
                    }
                }

                currentDirectory = _filesSystem.GetParentDirectory(currentDirectory);
            }

            

//            var dir = new DirectoryInfo(path);
//
//            while (dir != null)
//            {
//                if (Directory.Exists(Path.Combine(dir.FullName, "config")))
//                {
//                    var newConfigGroup = yamlFilesSystem.GetConfigGroupFromPath(Path.Combine(dir.FullName, "config"));
//
//                    newConfigGroups.Add(newConfigGroup);
//                }
//
//                dir = dir.Parent;
//            }

            newConfigGroups.Reverse();

            return newConfigGroups;
        }
    }
}
