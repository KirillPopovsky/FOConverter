using System;
using System.IO;

namespace FOConverter.scr.Common
{
    public class console
    {
        public static string Path
        {
            get { return Directory.GetCurrentDirectory() + @"\log.txt"; }
        }

        public static void createNewLog()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }

            File.Create(Path).Close();
        }

        public static void log(string format, params object[] args)
        {
            var str = String.Format(format, args);
            if (File.Exists(Path))
            {
                File.AppendAllText(Path, str);
            }

            Console.Out.WriteLine(format, args);
        }
    }
}