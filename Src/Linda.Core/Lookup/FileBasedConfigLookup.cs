﻿namespace Linda.Core.Lookup
{
    using System;
    using System.Web;

    public class FileBasedConfigLookup : BasedConfigLookupAbstract
    {
        private readonly IFilesSystem _filesSystem;

        private string _searchPatternRegEx = string.Empty;

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

        public FileBasedConfigLookup(string searchPatternRegEx)
            : this(new DefaultFilesSystem(), searchPatternRegEx)
        {
        }

        public FileBasedConfigLookup() : this(new DefaultFilesSystem())
        {
        }

        public FileBasedConfigLookup(IFilesSystem filesSystem, string searchPatternRegEx = null)
        {
            if (searchPatternRegEx == null)
            {
                SearchPatternRegEx = CheckIfWeb() ? "web.yml" : "app.yml";
            }
            else
            {
                SearchPatternRegEx = searchPatternRegEx;
            }
            
            _filesSystem = filesSystem;
        }

        public override ConfigGroup GetConfigGroup(ref string directory)
        {
            var result = new ConfigGroup();

            if (_filesSystem.Exists(directory))
            {
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