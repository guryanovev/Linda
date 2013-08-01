namespace Linda.Core.Lookup
{
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;

        public DirectoryBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem)
        {
            _filesSystem = filesSystem;
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            var configDirectoryPath = Path.Combine(directory, "config");
            if (_filesSystem.Exists(configDirectoryPath))
            {
                foreach (var file in _filesSystem.GetFiles(configDirectoryPath, "*.yml"))
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
