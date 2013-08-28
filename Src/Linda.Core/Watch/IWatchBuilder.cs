namespace Linda.Core.Watch
{
    using System;
    using System.IO;

    public interface IWatchBuilder
    {
        FileSystemWatcher GetWatcher(string path, Action<object, EventArgs> eventMethod, string filter = "*");
    }
}
