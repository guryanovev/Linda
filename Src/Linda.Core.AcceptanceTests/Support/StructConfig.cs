using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linda.Core.AcceptanceTests.Support
{
    internal struct SimpleStruct
    {
        public int Bar { get; set; }

        public string Baz { get; set; }
    }

    internal class StructConfig
    {
        public SimpleStruct ItIsStruct { get; set; }
    }
}
