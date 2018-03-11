using System.IO;
using Newtonsoft.Json;

namespace FOConverter.scr
{
    public struct Constants
    {
        public const string F3ToF4 = "F3ToF4";
        public const string FnvToF4 = "FnvToF4";
    }

    public struct ConfigData
    {
        public string sourceDataFolderPatch;
        public string f4DataFolderPath;
        public string convertType;
    }

    public class PathsPropsConfig
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
            var config = new ConfigData
            {
                convertType = Constants.F3ToF4 + " or " + Constants.FnvToF4,
                sourceDataFolderPatch = "D:\\Folder\\F3\\Data\\",
                f4DataFolderPath = "D:\\Folder\\F4\\Data\\"
            };
            var output = JsonConvert.SerializeObject(config);
            File.WriteAllText(configPath, output);
        }

        public void ReadConfig()
        {
            if (!File.Exists(configPath)) return;
            var output = File.ReadAllText(configPath);
            config = JsonConvert.DeserializeObject<ConfigData>(output);
        }
    }
}