using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace FOConverter.scr
{
    public abstract class EsmBaseAdapter
    {
        public abstract void Read(string _path);
    }

    public class EsmBinaryReader
    {
        FileStream fileStream;
        BinaryReader binaryReader;

        public EsmBinaryReader(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(path, " not found");
                return;
            }

            fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);
            Console.WriteLine("Opened: " + path);
        }

        public byte[] Read(int lenght, string message = "")
        {
            var bytes = binaryReader.ReadBytes(lenght);
            if (message != "")
            {
                Console.WriteLine(message + ": " + System.Text.Encoding.UTF8.GetString(bytes));
            }

            return bytes;
        }
    }

    public class F3baseAdaper : EsmBaseAdapter
    {
        public override void Read(string _path)
        {
            Console.WriteLine("Scanning path: " + _path);
            var path = _path + @"\fallout3.esm";
            EsmBinaryReader reader = new EsmBinaryReader(path);
            reader.Read(4, "Header signature");
            var recordSize = reader.Read(4);
            Console.WriteLine("Record size: " + BitConverter.ToInt32(recordSize, 0) + " ,0x" +
                              ByteArrayToString(recordSize));
            var flags = reader.Read(4);
            Console.WriteLine("Flags: " + ByteArrayToString(flags));
            var formID = reader.Read(4);
            Console.WriteLine("Form ID: " + ByteArrayToString(formID));

            var unknown1 = reader.Read(4);
            Console.WriteLine("Unk: " + ByteArrayToString(unknown1));
            var unknown2 = reader.Read(4);
            Console.WriteLine("Unk: " + ByteArrayToString(unknown2));


            reader.Read(4, "Header");
            Console.WriteLine("End");
        }

        public static string ByteArrayToString(byte[] _b)
        {
            var ba = _b.Reverse().ToArray();
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

    public class Converter
    {
        private EsmBaseAdapter esmBase;

        private EsmBaseAdapter getEsmAdapter(string type)
        {
            switch (type)
            {
                case Constants.F3ToF4:
                    return new F3baseAdaper();
                    break;
            }

            throw new Exception("unknown converter type");
        }

        public void Process(PathsPropsConfig configData)
        {
            esmBase = getEsmAdapter(configData.ConvertType);
            esmBase.Read(configData.SourceDataPath);
        }
    }
}