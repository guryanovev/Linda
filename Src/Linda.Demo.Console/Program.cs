namespace Linda.Demo.Console
{
    using System;
    using System.Collections.Generic;

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

    public class AnotherConfig
    {
        public string Foo { get; set; }

        public List<Struct> StructProp { get; set; }

        public struct Struct
        {
            public string Bar { get; set; }

            public string Baz { get; set; }
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Work with files");
            FileBasedConfig();

            Console.WriteLine("\n\nWork with directory");
            DirectoryBasedConfig();

            Console.ReadKey();
        }

        private static void FileBasedConfig()
        {
            var lookup = new FileBasedConfigLookup { SearchPattern = "App.yml" };

            var manager = new DefaultConfigManager(lookup);

            var config = manager.GetConfig<Configuration>();

            Console.WriteLine("Sku = {0}", config.Startup.SupportedRuntime.Sku);

            Console.WriteLine("Version = {0}", config.Startup.SupportedRuntime.Version);
        }

        private static void DirectoryBasedConfig()
        {
            var lookup = new DirectoryBasedConfigLookup { DirectoryName = "config", SearchPattern = "*.yml" };

            var manager = new DefaultConfigManager(lookup);

            var config = manager.GetConfig<AnotherConfig>();

            Console.WriteLine("Foo: {0}", config.Foo);

            foreach (var structP in config.StructProp)
            {
                Console.WriteLine("StructProperty:\n\tBaz: {0}\n\tBar: {1}\n", structP.Bar, structP.Baz);
            }
        }
    }
}
