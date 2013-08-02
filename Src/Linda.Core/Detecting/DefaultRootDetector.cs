namespace Linda.Core.Detecting
{
    using System;
    using System.IO;
    using System.Reflection;

    public class DefaultRootDetector : IRootDetector
    {
        public string GetConfigRoot()
        {
            // TODO исправить костыль
            var str = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;

            if (str == null) return AppDomain.CurrentDomain.BaseDirectory;

            return str;
        }
    }
}