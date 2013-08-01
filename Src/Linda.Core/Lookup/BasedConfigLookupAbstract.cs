namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BasedConfigLookupAbstract
    {
        public abstract ConfigGroup GetConfigGroup(ref string directory);
    }
}
