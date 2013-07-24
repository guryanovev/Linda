namespace Linda.Core
{
    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly string _configurationRoot;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConfigurationManager"/> class. 
        /// </summary>
        /// <param name="configurationRoot">
        /// full path to config root directory
        /// </param>
        public DefaultConfigurationManager(string configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public TConfiguration GetConfiguration<TConfiguration>()
        {
            // todo insert implementation here
            throw new System.NotImplementedException();
        }
    }
}