using System;
using System.Linq;
using FOConverter.scr.Common;

namespace FOConverter.scr.Records
{
    public class Record : BaseRecord
    {
        public Record(byte[] recordHeaderData, long dataAddress) : base(recordHeaderData, dataAddress)
        {
        }

        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nDataSize: {1},\nFlags: {2},\nFormID: {3},\nRevision: {4},\nFormVersion: {5},\nDataAddress: {6}\n",
                Signature, DataSize, Flags, FormId, Revision, FormVersion, _dataAddress);
        }

        //8..12
        public string Flags
        {
            get { return ByteConverter.ByteArrayToHexString(new ArraySegment<byte>(HeaderData, 8, 4).ToArray()); }
        }

        //12..16
        public string FormId
        {
            get { return ByteConverter.ByteArrayToHexString(new ArraySegment<byte>(HeaderData, 12, 4).ToArray()); }
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
    }
}