﻿using System;
using System.Collections.Generic;
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
            _dataAddress = dataAddress;
            ChildRecords = new BaseRecord[0];
        }

        public BaseRecord(byte[] recordHeaderData, long dataAddress, BaseRecord[] childRecords)
        {
            HeaderData = recordHeaderData;
            _dataAddress = dataAddress;
            ChildRecords = childRecords;
        }

        public override string ToString()
        {
            return String.Format(
                "Signature: {0},\nDataSize: {1},\nDataAddress: {2}\n",
                Signature, DataSize, _dataAddress);
        }

        protected readonly byte[] HeaderData;

        public BaseRecord[] ChildRecords;

        //0..4
        public string Signature
        {
            get { return ByteConverter.ByteArrayToString(new ArraySegment<byte>(HeaderData, 0, 4).ToArray()); }
        }

        //4..8
        public int DataSize
        {
            get { return BitConverter.ToInt32(HeaderData, 4); }
        }

        public long DataAddress
        {
            get { return _dataAddress; }
        }

        protected long _dataAddress;
        public byte[] Data;
    }
}