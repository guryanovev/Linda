namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;

    using YamlDotNet.RepresentationModel.Serialization;

    public class DefaultConfigManager : IConfigManager
    {
        private readonly IConfigSourceProvider _configSourceProvider;

        private readonly string _configRoot;

        private readonly IEnumerable<ConfigGroup> _configGroups;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigManager"/> class. 
        /// </summary>
        /// <param name="configRoot">
        /// full path to config root directory
        /// </param>
        /// <param name="configSourceProvider"></param>
        public DefaultConfigManager(string configRoot, IConfigSourceProvider configSourceProvider)
        {
            _configSourceProvider = configSourceProvider;
            _configRoot = configRoot;
            _configGroups = this._configSourceProvider.GetConfigGroups(this._configRoot);
        }

        public TConfig GetConfig<TConfig>()
        {
            var content = YamlFilesProvider.GetAllConfigContent(this._configGroups);

            return new Deserializer().Deserialize<TConfig>(new StringReader(content)); 
        }
    }
}