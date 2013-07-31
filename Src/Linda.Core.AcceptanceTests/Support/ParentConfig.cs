namespace Linda.Core.AcceptanceTests.Support
{
    using System;

    internal class ParentConfig : IEquatable<ParentConfig>
    {
        public SimpleConfig ChildConfig { get; set; }

        public bool Equals(ParentConfig other)
        {
            return this.ChildConfig.Equals(other.ChildConfig);
        }
    }
}