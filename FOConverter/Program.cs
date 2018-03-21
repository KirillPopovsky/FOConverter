using System;
using FOConverter.Properties;
using FOConverter.scr;
using FOConverter.scr.Common;
using FOConverter.scr.Esm;

namespace FOConverter
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            console.createNewLog();
            var config = new Configuration();
            config.ReadConfig();
            var main = new Main(config);
            main.ReadFiles();
        }
    }
}