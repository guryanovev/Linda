
namespace Linda.Core
{
    using System.IO;
    using System.Text;

    public static class YamlFilesProvider
    {
        public static string GetFileContent(string path)
        {
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }

            throw new FileNotFoundException();
        }

        public static string GetFolderConfigContent(string path)
        {
            var files = Directory.GetFiles(path, "*.yaml");

            var folderContent = new StringBuilder();

            foreach (var file in files)
            {
                folderContent.AppendLine(GetFileContent(file));
            }

            return folderContent.ToString();
        }
    }
}
