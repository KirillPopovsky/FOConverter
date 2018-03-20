using System.IO;
using Newtonsoft.Json;

namespace FOConverter.scr
{
    public struct PatchsConfigData
    {
        public string Fallout3DataFolderPath;
        public string FalloutNVDataFolderPath;
        public string Fallout4DataFolderPath;
    }

    public class PathsPropsConfig
    {
        private readonly string configPath = Directory.GetCurrentDirectory() + @"\configurations\PathsConfig.json";
        private PatchsConfigData _patchsConfig;

        public string Fallout3DataPath
        {
            get { return _patchsConfig.Fallout3DataFolderPath; }
        }

        public string Fallout4DataPath
        {
            get { return _patchsConfig.Fallout4DataFolderPath; }
        }

        public string FalloutNVDataPath
        {
            get { return _patchsConfig.FalloutNVDataFolderPath; }
        }


        public void CreateConfigTemplate()
        {
            var config = new PatchsConfigData
            {
                Fallout3DataFolderPath = @"D:\Games\Fallout3\Data\",
                FalloutNVDataFolderPath = @"D:\Games\FalloutNV\Data\",
                Fallout4DataFolderPath = @"D:\Games\Fallout4\Data\"
            };
            var output = JsonConvert.SerializeObject(config);
            File.WriteAllText(configPath, output);
        }

        public void ReadConfig()
        {
            if (!File.Exists(configPath)) return;
            var output = File.ReadAllText(configPath);
            _patchsConfig = JsonConvert.DeserializeObject<PatchsConfigData>(output);
        }
    }
}