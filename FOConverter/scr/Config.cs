using System;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;

namespace FOConverter.scr
{
    struct ConfigData
    {
        public string sourceDataFolderPatch;
        public string f4DataFolderPath;
        public string convertType;
    }

    public class Configuration
    {
        private readonly string configPath = Directory.GetCurrentDirectory() + @"\config.json";
        private ConfigData config;

        public string SourceDataPath
        {
            get { return config.sourceDataFolderPatch; }
        }

        public string F4DataPath
        {
            get { return config.f4DataFolderPath; }
        }

        public string ConvertType
        {
            get { return config.convertType; }
        }

        public void CreateConfigTemplate()
        {
            ConfigData config = new ConfigData();
            config.convertType = "F3ToF4|FnvToF4//remove one and this text";
            config.sourceDataFolderPatch = "D:\\Folder\\F3\\Data\\";
            config.f4DataFolderPath = "D:\\Folder\\F4\\Data\\";
            string output = JsonConvert.SerializeObject(config);
            File.WriteAllText(configPath, output);
        }

        public void ReadConfig()
        {
            if (File.Exists(configPath))
            {
                var output = File.ReadAllText(configPath);
                config = JsonConvert.DeserializeObject<ConfigData>(output);
            }
        }
    }
}