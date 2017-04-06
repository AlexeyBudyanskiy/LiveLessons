using System.Configuration;

namespace LiveLesson.WEB.Infrastructure
{
    public static class GlobalVariables
    {
        public static string ExampleVariable => ConfigurationManager.AppSettings["Example"];
        public static string ImagePath => ConfigurationManager.AppSettings["ImagePath"];
    }
}