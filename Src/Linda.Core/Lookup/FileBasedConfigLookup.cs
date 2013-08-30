namespace Linda.Core.Lookup
{
    using System;
    using System.Web;

    using Linda.Core.Watch;

    public class FileBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;
        private readonly IWatchBuilder _watchBuilder;

        private string _searchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";

        public FileBasedConfigLookup() : this(new DefaultFilesSystem(), new DefaultWatchBuilder())
        {
        }

        public FileBasedConfigLookup(IFilesSystem filesSystem, IWatchBuilder watchBuilder)
        {
            _searchPatternRegEx = CheckIfWeb() ? "web.yml" : "app.yml";

            _filesSystem = filesSystem;
            _watchBuilder = watchBuilder;
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

            if (_filesSystem.Exists(directory))
            {
                Watchers.Add(_watchBuilder.GetWatcher(directory, this.OnConfigChange, "*.yml"));

                foreach (var file in _filesSystem.GetFiles(directory, SearchPatternRegEx))
                {
                    var currentFile = file;
                    result.AddConfigSource(new ConfigSource(() => _filesSystem.GetFileContent(currentFile)));
                }
            }

            directory = _filesSystem.GetParentDirectory(directory);

            return result;
        }

        private static bool CheckIfWeb()
        {
            try
            {
                return HttpContext.Current != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}