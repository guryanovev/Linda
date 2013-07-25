namespace Linda.Core
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigGroup : IEnumerable<ConfigSource>
    {
        private readonly List<ConfigSource> _confSource;

        public ConfigGroup()
        {
            this._confSource = new List<ConfigSource>();
        }

        public void AddConfigSource(ConfigSource cs)
        {
            this._confSource.Add(cs);
        }

        public IEnumerator<ConfigSource> GetEnumerator()
        {
            return this._confSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
