namespace Linda.Core
{
    using System.Collections;
    using System.Collections.Generic;

    public class ConfigGroup : IEnumerable<ConfigSource>
    {
        private readonly List<ConfigSource> confSource;

        public ConfigGroup()
        {
            confSource = new List<ConfigSource>();
        }

        public void AddConfigSource(ConfigSource cs)
        {
            confSource.Add(cs);
        }

        public IEnumerator<ConfigSource> GetEnumerator()
        {
            return this.confSource.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
