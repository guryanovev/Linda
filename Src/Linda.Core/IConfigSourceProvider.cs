namespace Linda.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IConfigSourceProvider
    {
        List<ConfigGroup> GetCs(string path);
    }
}
