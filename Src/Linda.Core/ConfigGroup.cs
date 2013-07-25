namespace Linda.Core
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigGroup : IEnumerable<ConfigSource>
    {
        private readonly IList<ConfigSource> _configSource;

        public ConfigGroup()
        {
            this._configSource = new List<ConfigSource>();
        }

        public void AddConfigSource(ConfigSource cs)
        {
            this._configSource.Add(cs);
        }

        public IEnumerator<ConfigSource> GetEnumerator()
        {
            return this._configSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
