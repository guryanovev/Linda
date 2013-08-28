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
        
        public void OnConfigChange(object sender, EventArgs fileSystemEventArgs)
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

            Watchers.RunWatch();
        }

        public void Dispose()
        {
            for (int i = 0; i < Watchers.Length; i++)
            {
                Watchers[i].Dispose();
            }
        }

        protected abstract ConfigGroup GetConfigGroup(ref string directory);
    }
}
