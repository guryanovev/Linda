namespace Linda.Core.Detecting
{
    using System;
    using System.IO;

    public class DefaultRootDetector : IRootDetector
    {
        public string GetConfigRoot()
        {
            // TODO сделать для Web!
            var str = AppDomain.CurrentDomain.BaseDirectory;

            return str;
        }
    }
}