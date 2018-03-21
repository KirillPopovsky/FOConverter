using System.Collections.Generic;

namespace FOConverter.scr.Common
{
    public class ConvertConfig
    {
        public converter[] converters;
    }

    public struct converter
    {
        public string game;
        public string prefix;
        public string[] ignoreGroups;
        public string[] ignoreRecords;
        public Dictionary<string, string> replaceIDs;
        public string[] files;
    }
}