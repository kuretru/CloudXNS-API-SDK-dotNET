using System;
using System.IO;
using Kuretru.CloudXNSAPI.Model;
using Newtonsoft.Json;

namespace Kuretru.CloudXNSAPI.Controller
{
    internal static class ConfigurationController
    {
        public static APIConfiguration LoadConfigutaion()
        {
            APIConfiguration configuraion = null;
            string path = string.Format("{0}\\config.json", Environment.CurrentDirectory);
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string data = streamReader.ReadToEnd();
                    configuraion = JsonConvert.DeserializeObject<APIConfiguration>(data);
                }
            }
            if (configuraion == null)
            {
                configuraion = new APIConfiguration();
            }
            return configuraion;
        }
    }
}
