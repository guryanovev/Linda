namespace Linda.Demo.Web.Models
{
    using System.Collections.Generic;

    public class LindaPage
    {
        public string Title { get; set; }

        public string LinkToGithub { get; set; }

        public List<Tab> Tabs { get; set; }
    }

    public class Tab
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}