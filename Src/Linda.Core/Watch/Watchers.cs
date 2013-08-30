namespace Linda.Core.Watch
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Watchers : IDisposable
    {
        private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

        public int Length
        {
            get
            {
                return _watchers.Count;
            }
        }

        public FileSystemWatcher this[int index]
        {
            get
            {
                return _watchers[index];
            }
        }

        public void Add(FileSystemWatcher watcher)
        {
            if (watcher != null && watcher.Path != string.Empty)
            {
                _watchers.Add(watcher);
            }
        }

        public void RunWatch()
        {
            if (_watchers.Count != 0)
            {
                foreach (var watcher in _watchers)
                {
                    watcher.EnableRaisingEvents = true;
                }
            }
        }

        public void StopWatch()
        {
            foreach (var watcher in _watchers)
            {
                if (watcher != null)
                {
                    watcher.EnableRaisingEvents = false;
                }
            }
        }

        public void Dispose()
        {
            foreach (var fileSystemWatcher in _watchers)
            {
                fileSystemWatcher.Dispose();
            }
        }
    }
}
