namespace Linda.Core.AcceptanceTests.Support
{
    using System;
    using System.Collections.Generic;

    internal class ComplexConfig
    {
        public ComplexConfig()
        {
            StructProperty = new List<StructProp>();
            DictProperty = new Dictionary<string, Config>();
        }

        public string Bar { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, Config> DictProperty { get; set; }

        public List<StructProp> StructProperty { get; set; }

        public struct StructProp
        {
            public string Foo { get; set; }

            public Queue<double> Queue { get; set; }
        }

        public class Config
        {
            public Config()
            {
                Bytes = new List<byte>();
            }

            public int NumberOfConfig { get; set; }

            public bool IsProperty { get; set; }

            public double DoubleProperty { get; set; }

            public List<byte> Bytes { get; set; }
        }
    }
}
