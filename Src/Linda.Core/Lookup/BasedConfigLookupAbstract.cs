namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public abstract class BasedConfigLookupAbstract : IConfigLookup
    {
        private IEnumerable<ConfigGroup> _configGroups;  

        private List<CustomWatcher> _watchers = new List<CustomWatcher>();

        public event EventHandler<EventArgs> ConfigChange;

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

        protected List<CustomWatcher> Watchers
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
            EventHandler<EventArgs> handler = this.ConfigChange;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
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
        }

        public void Dispose()
        {
            for (int i = 0; i < Watchers.Count; i++)
            {
                Watchers[i].Dispose();
            }
        }

        protected abstract ConfigGroup GetConfigGroup(ref string directory);
    }
}
