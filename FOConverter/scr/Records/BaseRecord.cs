using System;
using System.Linq;
using FOConverter.scr.Common;

namespace FOConverter.scr.Records
{
    public class BaseRecord
    {
        public const short headerLength = 24;

        public BaseRecord(byte[] recordHeaderData, long dataAddress)
        {
            HeaderData = recordHeaderData;
            DataAddress = dataAddress;
        }

        protected readonly byte[] HeaderData;

        //0..4
        public string Signature
        {
            get { return EsmBinaryConverter.ByteArrayToString(new ArraySegment<byte>(HeaderData, 0, 4).ToArray()); }
        }

        //4..8
        public int DataSize
        {
            get { return BitConverter.ToInt32(HeaderData, 4); }
        }

        protected long DataAddress;
        protected byte[] Data;
    }
}