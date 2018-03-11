using FOConverter.scr;

namespace FOConverter
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var config = new PathsPropsConfig();
            config.ReadConfig();
            var converter = new Converter();
            converter.Process(config);
        }
    }
}