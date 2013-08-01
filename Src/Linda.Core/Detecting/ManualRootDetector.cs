namespace Linda.Core
{
    public class ManualRootDetector : IRootDetector
    {
        private readonly string _root;

        public ManualRootDetector(string root)
        {
            _root = root;
        }

        public string GetConfigRoot()
        {
            return _root;
        }
    }
}