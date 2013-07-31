namespace Linda.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

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

        public string RetrieveContent()
        {
            var resultContent = new StringBuilder();

            foreach (var configSource in _configSource)
            {
                resultContent.AppendLine(configSource.RetrieveContent());
            }

            return resultContent.ToString();
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
