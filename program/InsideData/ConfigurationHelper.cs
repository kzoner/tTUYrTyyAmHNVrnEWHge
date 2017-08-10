using System.Configuration;

namespace Inside.InsideData
{
    internal class ConfigurationHelper
    {
        public static string ReadKey(string key)
        {
            string value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            return value;
        }
    }
}
