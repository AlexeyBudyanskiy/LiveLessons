using System.Configuration;

namespace LiveLessons.WEB.Infrastructure
{
    public class GlobalVaribles
    {
        public static string ExampleVariable => ConfigurationManager.AppSettings["Example"];
    }
}