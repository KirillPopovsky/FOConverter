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

        public BaseRecord[] ReadSubBaseRecords(BaseRecord parentRecord)
        {
            List<BaseRecord> records = new List<BaseRecord>();
            if (parentRecord.Signature == "GRUP")
            {
                fileStream.Position = parentRecord.DataAddress;
                while (parentRecord.DataAddress + parentRecord.DataSize - Group.headerLength != fileStream.Position)
                {
                    var bytes = binaryReader.ReadBytes(BaseRecord.headerLength);
                    var record = new BaseRecord(bytes, fileStream.Position);
                    var subrecords = ReadSubBaseRecords(record);
                    record.SubRecords = subrecords;
                    fileStream.Position = parentRecord.DataAddress + record.DataSize +
                                          (record.Signature == "GRUP" ? 0 : BaseRecord.headerLength);
                    records.Add(record);
                }
            }
            else
            {
                console.log("Parent record is not group : {0}", parentRecord.Signature);
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
    }
}