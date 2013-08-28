namespace Linda.Core.Lookup
{
    using System;

    public interface IConfigLookup : IDisposable
    {
        event EventHandler<EventArgs> ConfigChange;

        void LoadConfigGroups(string path);

        string GetContent();
    }
}
