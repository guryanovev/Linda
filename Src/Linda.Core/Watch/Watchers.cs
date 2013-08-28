namespace Linda.Core.Watch
{
    using System.Collections.Generic;
    using System.IO;

    public class Watchers
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
            if (watcher.Path != string.Empty)
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
    }
}
