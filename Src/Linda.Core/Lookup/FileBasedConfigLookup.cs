namespace Linda.Core.Lookup
{
    using System;
    using System.Web;

    public class FileBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;

        private string _searchPattern = "*.yml";

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

        public FileBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public FileBasedConfigLookup(IFilesSystem filesSystem)
        {
            if (CheckIfWeb())
            {
                SearchPattern = "web.yml";
            }
            else
            {
                SearchPattern = "app.yml";
            }
            _filesSystem = filesSystem;
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            if (_filesSystem.Exists(directory))
            {
                foreach (var file in _filesSystem.GetFiles(directory, SearchPattern))
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