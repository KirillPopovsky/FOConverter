using System.Collections.Generic;

namespace FOConverter.scr.Common
{
    public class ConvertConfig
    {
        public game[] games;
    }

    public struct game
    {
        public string name;
        public string prefix;
        public string[] ignoreGroups;
        public string[] ignoreRecords;
        public Dictionary<string, string> replaceIDs;
        public string[] files;
    }
}