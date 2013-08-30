namespace Linda.Core.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public interface IConfigLookup : IDisposable
    {
        event EventHandler<FileSystemEventArgs> ConfigChange;

        IEnumerable<ConfigGroup> ConfigGroups { get; set; }

        void LoadConfigGroups(string path);

        string GetContent();
    }
}
