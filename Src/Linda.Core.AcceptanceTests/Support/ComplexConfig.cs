namespace Linda.Core.AcceptanceTests.Support
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class ComplexConfig : IEquatable<ComplexConfig>
    {
        public ComplexConfig()
        {
            StructProperty = new List<Struct>();
            DictProperty = new Dictionary<string, Config>();
        }

        public string Bar { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, Config> DictProperty { get; set; }

        public List<Struct> StructProperty { get; set; }

        public bool Equals(ComplexConfig other)
        {
            if (DictProperty.Count != other.DictProperty.Count)
            {
                return false;
            }

            if (StructProperty.Count != other.StructProperty.Count)
            {
                return false;
            }

            bool barEquals = Bar == other.Bar;
            bool dateEquals = Date == other.Date;

            foreach (var dict in DictProperty)
            {
                if (!other.DictProperty.ContainsKey(dict.Key))
                {
                    return false;
                }

                if (!other.DictProperty[dict.Key].Equals(dict.Value))
                {
                    return false;
                }
            }

            bool listEquals = StructProperty.SequenceEqual(other.StructProperty);

            return barEquals && dateEquals && listEquals;
        }

        public struct Struct : IEquatable<Struct>
        {
            public string Foo { get; set; }

            public List<double> List { get; set; }

            public bool Equals(Struct other)
            {
                return List.SequenceEqual(other.List) && Foo == other.Foo;
            }
        }

        public class Config : IEquatable<Config>
        {
            public Config()
            {
                Bytes = new List<byte>();
            }

            public int NumberOfConfig { get; set; }

            public bool IsProperty { get; set; }

            public double DoubleProperty { get; set; }

            public List<byte> Bytes { get; set; }

            public bool Equals(Config other)
            {
                return Bytes.SequenceEqual(other.Bytes)
                    && (Math.Abs(DoubleProperty - other.DoubleProperty) < 0.0001)
                       && (IsProperty == other.IsProperty)
                       && (NumberOfConfig == other.NumberOfConfig);
            }
        }
    }
}
