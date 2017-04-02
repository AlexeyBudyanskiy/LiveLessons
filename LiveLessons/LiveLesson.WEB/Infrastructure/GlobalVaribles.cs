using System.Configuration;

namespace LiveLesson.WEB.Infrastructure
{
    public class GlobalVaribles
    {
        public static string ExampleVariable => ConfigurationManager.AppSettings["Example"];
    }
}