namespace Linda.Core
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigGroup : IEnumerable<ConfigSource>
    {
        private readonly IList<ConfigSource> _configSource;

        public ConfigGroup()
        {
            _configSource = new List<ConfigSource>();
        }

        public void AddConfigSource(ConfigSource cs)
        {
            _configSource.Add(cs);
        }

        public IEnumerator<ConfigSource> GetEnumerator()
        {
            return _configSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
