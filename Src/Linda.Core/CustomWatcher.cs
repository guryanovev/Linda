namespace Linda.Core
{
    using System;
    using System.IO;

    public class CustomWatcher : IDisposable
    {
        private readonly FileSystemWatcher _watcher;

        private EventMethod _created;
        private EventMethod _changed;
        private EventMethod _deleted;
        private EventMethod _renamed;

        private NotifyFilters _filters = NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                        | NotifyFilters.DirectoryName;

        public CustomWatcher()
        {
        }

        public CustomWatcher(string path)
        {   
            _watcher = new FileSystemWatcher(path);
        }

        public CustomWatcher(string path, string searchPattern)
        {
            _watcher = new FileSystemWatcher(path, searchPattern);
        }

        public CustomWatcher(string path, EventMethod eventMethod)
        {
            _watcher = new FileSystemWatcher(path);
            Created = eventMethod;
            Changed = eventMethod;
            Deleted = eventMethod;
            Renamed = eventMethod;
        }

        public delegate void EventMethod(object sender, FileSystemEventArgs args);

        public EventMethod Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;
                _watcher.Created += new FileSystemEventHandler(_created);
            }
        }

        public EventMethod Changed
        {
            get
            {
                return _changed;
            }

            set
            {
                _changed = value;
                _watcher.Changed += new FileSystemEventHandler(_changed);
            }
        }

        public EventMethod Deleted
        {
            get
            {
                return _deleted;
            }

            set
            {
                _deleted = value;
                _watcher.Deleted += new FileSystemEventHandler(_deleted);
            }
        }

        public EventMethod Renamed
        {
            get
            {
                return _renamed;
            }

            set
            {
                _renamed = value;
                _watcher.Renamed += new RenamedEventHandler(_renamed);
            }
        }

        public NotifyFilters Filters
        {
            get
            {
                return _filters;
            }

            set
            {
                _filters = value;
            }
        }

        public void RunWatch()
        {
            _watcher.NotifyFilter = Filters;

            _watcher.EnableRaisingEvents = true;
        }

        public void StopWatch()
        {
            _watcher.EnableRaisingEvents = false;
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
