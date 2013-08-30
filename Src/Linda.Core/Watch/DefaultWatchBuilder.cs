namespace Linda.Core.Watch
{
    using System;
    using System.IO;

    public class DefaultWatchBuilder : IWatchBuilder
    {
        public FileSystemWatcher GetWatcher(string path, Action<object, FileSystemEventArgs> eventMethod, string filter = "*")
        {
            var watcher = new FileSystemWatcher(path, filter);
            watcher.Created += new FileSystemEventHandler(eventMethod);
            watcher.Changed += new FileSystemEventHandler(eventMethod);
            watcher.Deleted += new FileSystemEventHandler(eventMethod);
            watcher.Renamed += new RenamedEventHandler(eventMethod);
            return watcher;
        }
    }
}
