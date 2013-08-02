namespace Linda.Core.Lookup
{
    using System.IO;

    public class DirectoryBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;
        private string _searchPatternRegEx = string.Empty;
        private string _directoryName = "config";

        public DirectoryBasedConfigLookup(string searchPatternRegEx)
            : this(new DefaultFilesSystem(), searchPatternRegEx)
        {
        }

        public DirectoryBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem, string searchPatternRegEx = null)
        {
            SearchPatternRegEx = searchPatternRegEx ?? "[a-zA-Z0-9\\._-]*.yml";

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

        public string SearchPatternRegEx
        {
            get
            {
                return _searchPatternRegEx;
            }

            set
            {
                _searchPatternRegEx = value;
            }
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            var configDirectoryPath = Path.Combine(directory, DirectoryName);
            if (_filesSystem.Exists(configDirectoryPath))
            {
                foreach (var file in _filesSystem.GetFiles(configDirectoryPath, SearchPatternRegEx))
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
