using FOConverter.scr;
using FOConverter.scr.Common;

namespace FOConverter
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            console.createNewLog();
            var config = new PathsPropsConfig();
            config.ReadConfig();
            var converter = new Converter();
            converter.Process(config);
        }
    }
}