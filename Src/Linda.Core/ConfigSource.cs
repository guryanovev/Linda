namespace Linda.Core
{
    public class ConfigSource
    {
        private readonly string _path;

        public ConfigSource(string path)
        {
            _path = path;
        }

        public string Path
        {
            get
            {
                return _path;
            }
        }
    }
}
