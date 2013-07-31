namespace Linda.Core
{
    using System;

    public class ConfigSource
    {
        private readonly Func<string> _contentProvider;

        public ConfigSource(string content) : this(() => content)
        {
        }

        public ConfigSource(Func<string> contentProvider)
        {
            _contentProvider = contentProvider;
        }

        public string RetrieveContent()
        {
            return _contentProvider();
        }
    }
}
