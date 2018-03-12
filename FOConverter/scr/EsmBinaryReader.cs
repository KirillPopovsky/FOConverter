using System;
using System.IO;
using System.Linq;
using System.Text;
using FOConverter.scr.Records;

namespace FOConverter.scr
{
    public class EsmBinaryReader
    {
        FileStream fileStream;
        BinaryReader binaryReader;

        public long Address
        {
            get { return fileStream.Position; }
        }

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

        public byte[] ReadBytes(int lenght, long position = -1)
        {
            byte[] bytes;
            if (position != -1)
            {
                var _pos = fileStream.Position;
                fileStream.Position = position;
                bytes = binaryReader.ReadBytes(lenght);
                fileStream.Position = _pos;
            }
            else
            {
                bytes = binaryReader.ReadBytes(lenght);
            }


            return bytes;
        }

        public BaseRecord ReadRecordHeader()
        {
            var bytes = ReadBytes(BaseRecord.headerLength);
            var record = new BaseRecord(bytes, fileStream.Position);
            fileStream.Position += record.DataSize;
            var h = ReadBytes(4);
            Console.WriteLine(System.Text.Encoding.Default.GetString(h));
            return record;
        }
    }
}