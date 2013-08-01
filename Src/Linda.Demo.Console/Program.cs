namespace Linda.Demo.Console
{
    using System;

    using System.Configuration;

    using Linda.Core;

    public class SimpleConfig
    {
        public string Foo { get; set; }

        public string Bar { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {


            var manager = new DefaultConfigManager();

            var config = manager.GetConfig<SimpleConfig>();

            Console.WriteLine(config.Bar);
        }
    }
}
