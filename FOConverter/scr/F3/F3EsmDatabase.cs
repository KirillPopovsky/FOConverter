using System;
using System.Collections.Generic;
using System.Linq;
using FOConverter.scr.Common;
using FOConverter.scr.Groups;
using FOConverter.scr.Records;

namespace FOConverter.scr.F3
{
    public abstract class EsmDatabase
    {
        public abstract void Read(string _path);
    }


    public class F3EsmDatabase : EsmDatabase
    {
        private Record TES4;

        private Dictionary<string, TopLevelGroup> topLevelGroups;
        private string[] topLevelGroupsKeys;

        public override void Read(string _path)
        {
            var esmbr = new EsmBinaryReader(_path + "\\Fallout3.esm");
            TES4 = esmbr.ReadRecordHeader();
            topLevelGroups = new Dictionary<string, TopLevelGroup>();
            console.log("\nTES4 Record:\n" + TES4 + " \n");
            while (!esmbr.EndOfFile)
            {
                var group = esmbr.ReadTopLevel();
                topLevelGroups.Add(group.Lable, group);
            }


            topLevelGroupsKeys = topLevelGroups.Keys.ToArray();
            foreach (var grup in topLevelGroupsKeys)
            {
                var recs = esmbr.ReadSubBaseRecords(topLevelGroups[grup]);
                console.log("\nRecs of {0} :\n\n{1}", grup, string.Join(" \n", recs.AsEnumerable()));
                topLevelGroups[grup].SubRecords = recs;
            }

            console.log("Groups: \n{0}", string.Join(", ", topLevelGroupsKeys));
        }
    }
}