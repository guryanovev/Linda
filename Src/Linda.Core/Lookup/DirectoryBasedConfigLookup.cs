namespace Linda.Core.Lookup
{
    using System.Collections.Generic;
    using System.IO;

    public class DirectoryBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;
        private string _searchPattern = "*.yml";
        private string _directoryName = "config";

        public DirectoryBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem)
        {
            _filesSystem = filesSystem;
        }

        public string DirectoryName
        {
            get
            {
                return _directoryName;
            }

            set
            {
                _directoryName = value;
            }
        }

        public string SearchPattern
        {
            get
            {
                return _searchPattern;
            }

            set
            {
                _searchPattern = value;
            }
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            var configDirectoryPath = Path.Combine(directory, DirectoryName);
            if (_filesSystem.Exists(configDirectoryPath))
            {
                foreach (var file in _filesSystem.GetFiles(configDirectoryPath, SearchPattern))
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
