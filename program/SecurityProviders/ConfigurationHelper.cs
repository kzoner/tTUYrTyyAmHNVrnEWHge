using System.Configuration;

namespace Inside.SecurityProviders
{
    class ConfigurationHelper
    {
        public static string ReadKey(string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);

            return value;            
        }
    }
}
