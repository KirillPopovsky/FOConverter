using System;
using System.Linq;
using System.Text;

namespace FOConverter.scr.Groups
{
    public class TopLevelGroup : Group
    {
        public TopLevelGroup(byte[] data, long address) : base(data, address)
        {
        }

        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nLable: {5},\nDataSize: {1},\nGroupType: {2},\nStamp: {3},\nDataAddress: {4}\n",
                Signature, DataSize, GroupType, Stamp, _dataAddress, Lable);
        }

        public string Lable
        {
            get { return Encoding.Default.GetString(_lable); }
        }
    }
}