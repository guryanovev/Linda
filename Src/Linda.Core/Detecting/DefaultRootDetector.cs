namespace Linda.Core.Detecting
{
    using System;
    using System.Web;

    public class DefaultRootDetector : IRootDetector
    {
        public string GetConfigRoot()
        {
            var isWeb = CheckIfWeb();

            var result = isWeb ? HttpContext.Current.Server.MapPath("~/bin") : AppDomain.CurrentDomain.BaseDirectory;

            return result;
        }

        private static bool CheckIfWeb()
        {
            try
            {
                return HttpContext.Current != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}