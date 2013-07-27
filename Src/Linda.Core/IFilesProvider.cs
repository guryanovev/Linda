namespace Linda.Core
{
    public interface IFilesProvider
    {
        ConfigGroup GetConfigGroupFromPath(string path);
    }
}
