namespace Linda.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Linda.Core.Detecting;
    using Linda.Core.Lookup;
    using Linda.Core.Yaml;

    public class DefaultConfigManager : IConfigManager, IDisposable
    {
        private readonly IConfigLookup _configLookup;
        private readonly IYamlDeserializer _deserializer;
        private readonly IRootDetector _rootDetector;
        private readonly object _obj = new object();

        public DefaultConfigManager() : this(new FileBasedConfigLookup(), new CustomDeserializer(), new DefaultRootDetector())
        {
        }

        public DefaultConfigManager(IConfigLookup configLookup)
            : this(configLookup, new CustomDeserializer(), new DefaultRootDetector())
        {
        }

        public DefaultConfigManager(string configRoot) : this(new FileBasedConfigLookup(), new CustomDeserializer(), new ManualRootDetector(configRoot))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configLookup"></param>
        /// <param name="deserializer"></param>
        /// <param name="rootDetector"></param>
        public DefaultConfigManager(IConfigLookup configLookup, IYamlDeserializer deserializer, IRootDetector rootDetector)
        {
            _configLookup = configLookup;
            _deserializer = deserializer;
            _rootDetector = rootDetector;
            _configLookup.LoadConfigGroups(_rootDetector.GetConfigRoot());
        }

        public event EventHandler<EventArgs> MyEvent;

        public void OnMyEvent()
        {
            lock (_obj)
            {
                _configLookup.LoadConfigGroups(_rootDetector.GetConfigRoot());
                this.MyEvent(this, new EventArgs());
            }
        }

        public TConfig GetConfig<TConfig>() where TConfig : new()
        {
            var resultConfig = _deserializer.Deserialize<TConfig>(_configLookup.GetContent());

            return resultConfig;
        }

        public void WatchForConfig<TConfig>(Action<TConfig> callback) where TConfig : new()
        {
            _configLookup.ConfigChange += (sender, e) => this.OnMyEvent();
            callback(new TConfig());
            this.MyEvent += (sender, args) => callback(new TConfig());
        }

        public void Dispose()
        {
            _configLookup.Dispose();
        }
    }
}