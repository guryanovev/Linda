namespace Linda.Core.Tests
{
    using Linda.Core.Tests.Support;

    public class SimpleConfigTests
    {
        public void Test_CanLoad()
        {
            IConfigurationManager manager = new DefaultConfigurationManager();

            var mySimpleConfig = manager.GetConfiguration<SimpleConfig>();
            CreateConnections(mySimpleConfig.ConnectionString);
            CreateServices(mySimpleConfig.WebSiteBaseUrl);
        }

        private void CreateServices(string webSiteBaseUrl)
        {
            throw new System.NotImplementedException();
        }

        private void CreateConnections(string connectionString)
        {
            throw new System.NotImplementedException();
        }
    }
}