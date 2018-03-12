using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;

namespace FOConverter.scr.Records
{
    public class EsmBinaryConverter
    {
        public static string ByteArrayToHexString(byte[] _b)
        {
            var ba = _b.Reverse().ToArray();
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ByteArrayToString(byte[] _b)
        {
            return System.Text.Encoding.Default.GetString(_b);
        }
    }

    public class BaseRecord
    {
        public const short headerLength = 24;

        public BaseRecord(byte[] recordHeaderData)
        {
            HeaderData = recordHeaderData;
        }

        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nDataSize: {1},\nFlags: {2},\nFormID: {3},\nRevision: {4},\nFormVersion: {5},\n",
                Signature, DataSize, Flags, FormId, Revision, FormVersion);
        }

        public byte[] HeaderData;

        public string Signature
        {
            get { return EsmBinaryConverter.ByteArrayToString(new ArraySegment<byte>(HeaderData, 0, 4).ToArray()); }
        }

        public int DataSize
        {
            get { return BitConverter.ToInt32(HeaderData, 4); }
        }

        public int Flags;
        public byte[] FormId;
        public int Revision;
        public short FormVersion;
        public short Unknown;
        public long DataAddress;
        public byte[] Data;
    }
}