namespace Linda.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using YamlDotNet.RepresentationModel.Serialization;

    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly IConfigSourceProvider _configSourceProvider;

        private readonly string _configurationRoot;

        private List<ConfigGroup> _confCont;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationManager"/> class. 
        /// </summary>
        /// <param name="configurationRoot">
        /// full path to config root directory
        /// </param>
        /// <param name="configSourceProvider"></param>
        public DefaultConfigurationManager(string configurationRoot, IConfigSourceProvider configSourceProvider)
        {
            this._configSourceProvider = configSourceProvider;
            _configurationRoot = configurationRoot;
        }

        public TConfiguration GetConfiguration<TConfiguration>()
        {
            this._confCont = this._configSourceProvider.GetCs(_configurationRoot);

            var content = YamlFilesProvider.GetAllConfigContent(this._confCont);

            return new Deserializer().Deserialize<TConfiguration>(new StringReader(content)); 
        }
    }
}