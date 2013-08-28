﻿namespace Linda.Core.Lookup
{
    using System;
    using System.Web;

    public class FileBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;

        private string _searchPatternRegEx = "[a-zA-Z0-9\\._-]*.yml";

        public FileBasedConfigLookup()
            : this(new DefaultFilesSystem())
        {
        }

        public FileBasedConfigLookup(IFilesSystem filesSystem)
        {
            _searchPatternRegEx = CheckIfWeb() ? "web.yml" : "app.yml";

            _filesSystem = filesSystem;
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
                var watcher = new CustomWatcher(directory, this.OnConfigChange);
                watcher.RunWatch();
                Watchers.Add(watcher);

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