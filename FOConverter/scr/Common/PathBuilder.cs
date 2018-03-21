namespace FOConverter.scr.Common
{
    public class PathBuilder
    {
        public static string getPath(Configuration config, string game, string fileName)
        {
            switch (game)
            {
                case "Fallout3":
                    return config.Fallout3DataPath + fileName;
                case "FalloutNV":
                    return config.FalloutNVDataPath + fileName;
                case "Fallout4":
                    return config.Fallout4DataPath + fileName;
            }

            return "";
        }
    }
}