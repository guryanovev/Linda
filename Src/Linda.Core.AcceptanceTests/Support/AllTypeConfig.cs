using System;

namespace Linda.Core.AcceptanceTests.Support
{
    using System.Collections.Generic;

    internal class AllTypeConfig
    {
        public AllTypeConfig()
        {
            ByteArrayProp = new List<byte>();
        }

        public bool BoolProp { get; set; }

        public int IntProp { get; set; }

        public double DoubleProp { get; set; }

        public float FloatProp { get; set; }

        public string StringProp { get; set; }

        public DateTime DateProp { get; set; }

        public List<byte> ByteArrayProp { get; set; }
    }
}
