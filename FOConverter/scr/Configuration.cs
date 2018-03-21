using System.IO;
using FOConverter.scr.Common;
using Newtonsoft.Json;

namespace FOConverter.scr
{
    public class Configuration
    {
        private readonly string pathsConfig__ = Directory.GetCurrentDirectory() + @"\configurations\PathsConfig.json";

        private readonly string convertConfig__ =
            Directory.GetCurrentDirectory() + @"\configurations\ConvertConfig.json";

        private PathsConfig _pathsConfig;
        private ConvertConfig _converConfig;

        public string Fallout3DataPath
        {
            get { return _pathsConfig.Fallout3DataFolderPath; }
        }

        public string Fallout4DataPath
        {
            get { return _pathsConfig.Fallout4DataFolderPath; }
        }

        public string FalloutNVDataPath
        {
            get { return _pathsConfig.FalloutNVDataFolderPath; }
        }

        public void ReadConfig()
        {
            if (!File.Exists(pathsConfig__)) return;
            var paths = File.ReadAllText(pathsConfig__);
            _pathsConfig = JsonConvert.DeserializeObject<PathsConfig>(paths);

            if (!File.Exists(convertConfig__)) return;
            var converts = File.ReadAllText(convertConfig__);
            _converConfig = JsonConvert.DeserializeObject<ConvertConfig>(converts);
        }
    }
}