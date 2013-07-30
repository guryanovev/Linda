namespace Linda.Core.AcceptanceTests.Support
{
    using System;
    public class SimpleConfig : IEquatable<SimpleConfig>
    {
        public string Foo { get; set; }

        public string Bar { get; set; }

        public bool Equals(SimpleConfig other)
        {
            return other.Bar == this.Bar && other.Foo == this.Foo;
        }
    }
}