namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;

    public interface IConfigLookup : IDisposable
    {
        event EventHandler<EventArgs> ConfigChange;

        IEnumerable<ConfigGroup> ConfigGroups { get; set; }

        void LoadConfigGroups(string path);

        string GetContent();
    }
}
