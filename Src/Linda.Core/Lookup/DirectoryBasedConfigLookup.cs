namespace Linda.Core.Lookup
{
    using System.IO;
    using Linda.Core;
    using Linda.Core.Watch;

    public class DirectoryBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;
        private readonly IWatchBuilder _watchBuilder;
        private string _searchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";
        private string _directoryName = "config";

        public DirectoryBasedConfigLookup() : this(new DefaultFilesSystem(), new DefaultWatchBuilder())
        {
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem)
        {
            _filesSystem = filesSystem;
        }

        public DirectoryBasedConfigLookup(IFilesSystem filesSystem, IWatchBuilder watchBuilder)
        {
            _filesSystem = filesSystem;
            _watchBuilder = watchBuilder;
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

        protected override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            var configDirectoryPath = Path.Combine(directory, DirectoryName);
            if (_filesSystem.Exists(configDirectoryPath))
            {
                Watchers.Add(_watchBuilder.GetWatcher(configDirectoryPath, this.OnConfigChange, "*.yml"));

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
