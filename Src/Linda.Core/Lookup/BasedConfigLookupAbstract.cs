namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Linda.Core.Watch;

    public abstract class BasedConfigLookupAbstract : IConfigLookup
    {
        private IEnumerable<ConfigGroup> _configGroups;

        private Watchers _watchers = new Watchers();

        public event EventHandler<FileSystemEventArgs> ConfigChange;

        public IEnumerable<ConfigGroup> ConfigGroups
        {
            get
            {
                return _configGroups;
            }

            set
            {
                _configGroups = value;
            }
        }

        public Watchers Watchers
        {
            get
            {
                return _watchers;
            }

            set
            {
                _watchers = value;
            }
        }

        public void OnConfigChange(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            if (fileSystemEventArgs != null && !CheckChangedFileName(fileSystemEventArgs.Name))
            {
                return;
            }

            EventHandler<FileSystemEventArgs> handler = ConfigChange;
            if (handler != null)
            {
                handler(this, EventArgs.Empty as FileSystemEventArgs);
            }
        }

        public string GetContent()
        {
            var content = string.Empty;

            foreach (var configGroup in _configGroups)
            {
                content += configGroup.RetrieveContent();
            }

            return content;
        }

        public void LoadConfigGroups(string path)
        {
            var result = new List<ConfigGroup>();

            var currentDirectory = path;
            while (currentDirectory != null)
            {
                result.Add(GetConfigGroup(ref currentDirectory));
            }

            result.Reverse();

            _configGroups = result;

            Watchers.RunWatch();
        }

        public void Dispose()
        {
            Watchers.Dispose();
        }

        protected abstract bool CheckChangedFileName(string name);

        protected abstract ConfigGroup GetConfigGroup(ref string directory);
    }
}
