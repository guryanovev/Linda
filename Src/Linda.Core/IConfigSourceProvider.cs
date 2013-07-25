namespace Linda.Core
{
    using System.Collections.Generic;

    public interface IConfigSourceProvider
    {
<<<<<<< HEAD
        IEnumerable<ConfigGroup> GetConfigGroups(string path);
=======
        List<ConfigGroup> GetConfigGroups(string path);
>>>>>>> e924abedf9a41f75d58dc6413ccb6c84f5bc3442
    }
}
