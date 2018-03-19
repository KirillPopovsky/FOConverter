using System;
using System.Collections.Generic;
using System.IO;
using FOConverter.scr.Common;
using FOConverter.scr.Groups;
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

        public bool EndOfFile
        {
            get { return fileStream.Position == fileStream.Length; }
        }

        public EsmBinaryReader(string path)
        {
            if (!File.Exists(path))
            {
                console.log(path, " not found");
                return;
            }

            fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            binaryReader = new BinaryReader(fileStream);
            console.log("Opened: " + path);
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

        public Record ReadRecordHeader()
        {
            var bytes = ReadBytes(BaseRecord.headerLength);
            var record = new Record(bytes, fileStream.Position);
            fileStream.Position += record.DataSize;
            return record;
        }


        public BaseRecord[] ReadChildBaseRecords(BaseRecord parentRecord)
        {
            List<BaseRecord> records = new List<BaseRecord>();

            if (parentRecord.Signature == Group.SignatureGRUP)
            {
                long pos = parentRecord.DataAddress;
                fileStream.Position = pos;
                while (parentRecord.DataAddress + parentRecord.DataSize - Group.headerLength != fileStream.Position)
                {
                    var bytes = binaryReader.ReadBytes(BaseRecord.headerLength);
                    var record = new BaseRecord(bytes, fileStream.Position);

                    pos = pos + BaseRecord.headerLength + record.DataSize -
                          (record.Signature == Group.SignatureGRUP ? Group.headerLength : 0);

                    var childRecords = ReadChildBaseRecords(record);
                    record.ChildRecords = childRecords;

                    fileStream.Position = pos;
                    records.Add(record);
                }
            }
            else
            {
                console.debug("Parent record is not group : \n{0}", parentRecord);
            }

            return records.ToArray();
        }

        public Group ReadGroupHeader()
        {
            var bytes = ReadBytes(Group.headerLength);
            var group = new Group(bytes, fileStream.Position);
            fileStream.Position += group.DataSize - Group.headerLength;
            return group;
        }

        public TopLevelGroup ReadTopLevel()
        {
            var bytes = ReadBytes(Group.headerLength);
            var group = new TopLevelGroup(bytes, fileStream.Position);
            fileStream.Position += group.DataSize - Group.headerLength;
            return group;
        }

        public void ReadData(ref BaseRecord record)
        {
            if (record.Signature == Group.SignatureGRUP)
            {
                console.debug("record is group {0}", record);
                return;
            }

            fileStream.Position = record.DataAddress;
            byte[] data = binaryReader.ReadBytes(record.DataSize);
            record.Data = data;
        }
    }
}