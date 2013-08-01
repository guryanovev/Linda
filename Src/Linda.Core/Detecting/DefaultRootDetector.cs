namespace Linda.Core.Detecting
{
    using System;

    public class DefaultRootDetector : IRootDetector
    {
        public string GetConfigRoot()
        {
//            if (isWebApplication)
//            {
//                return GetWebRelative("bin");
//            }
//            else
//            {
//                return GetCurrentWorkingDirectory();
//            }

            throw new NotImplementedException();
        }
    }
}