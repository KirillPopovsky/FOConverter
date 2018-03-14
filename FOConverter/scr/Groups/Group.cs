using System;
using System.Linq;
using FOConverter.scr.Common;
using FOConverter.scr.Records;

namespace FOConverter.scr.Groups
{
    public class Group : BaseRecord
    {
        public Group(byte[] data, long address) : base(data, address)
        {
        }
        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nDataSize: {1},\nGroupType: {2},\nStamp: {3},\nDataAddress: {4}\n",
                Signature, DataSize, GroupType, Stamp, DataAddress);
        }

        //8..12
        protected byte[] _lable
        {
            get { return new ArraySegment<byte>(HeaderData, 8, 4).ToArray(); }
        }

        //12..16
        public int GroupType
        {
            get { return BitConverter.ToInt32(HeaderData, 12); }
        }

        //16..18
        public short Stamp
        {
            get { return BitConverter.ToInt16(HeaderData, 16); }
        }

        //18..24
        public byte[] Unknown
        {
            get { return new ArraySegment<byte>(HeaderData, 18, 6).ToArray(); }
        }
    }
}