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

        public BaseRecord(byte[] recordHeaderData, long dataAddress)
        {
            HeaderData = recordHeaderData;
            DataAddress = dataAddress;
        }

        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nDataSize: {1},\nFlags: {2},\nFormID: {3},\nRevision: {4},\nFormVersion: {5},\nDataAddress: {6}\n",
                Signature, DataSize, Flags, FormId, Revision, FormVersion, DataAddress);
        }

        public byte[] HeaderData;

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

        //8..12
        public string Flags
        {
            get { return EsmBinaryConverter.ByteArrayToHexString(new ArraySegment<byte>(HeaderData, 8, 4).ToArray()); }
        }

        //12..16
        public string FormId
        {
            get { return EsmBinaryConverter.ByteArrayToHexString(new ArraySegment<byte>(HeaderData, 12, 4).ToArray()); }
        }

        //16..20
        public int Revision
        {
            get { return BitConverter.ToInt32(HeaderData, 16); }
        }

        //20...22
        public short FormVersion
        {
            get { return BitConverter.ToInt16(HeaderData, 20); }
        }

        //22..24
        public short Unknown
        {
            get { return BitConverter.ToInt16(HeaderData, 22); }
        }

        public long DataAddress;
        public byte[] Data;
    }
}