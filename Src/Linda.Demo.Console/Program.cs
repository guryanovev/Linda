namespace Linda.Demo.Console
{
    using System;

    using Linda.Core;
    using Linda.Core.Lookup;

    public struct Supported
    {
        public string Version { get; set; }

        public string Sku { get; set; }
    }

    public class StartUp
    {
        public Supported SupportedRuntime { get; set; }
    }

    public class Configuration
    {
        public StartUp Startup { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var lookup = new FileBasedConfigLookup { SearchPattern = "App.yml" };

            var manager = new DefaultConfigManager(lookup);

            var c = new Configuration();
            
            var config = manager.GetConfig<Configuration>();

            Console.WriteLine(config.Startup.SupportedRuntime.Sku);

            Console.WriteLine(config.Startup.SupportedRuntime.Version);

        }
    }
}
