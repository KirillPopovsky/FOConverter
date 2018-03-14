using System;
using System.Collections.Generic;
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

        public override void Read(string _path)
        {
            var esmbr = new EsmBinaryReader(_path + "\\Fallout3.esm");
            TES4 = esmbr.ReadRecordHeader();
            topLevelGroups = new Dictionary<string, TopLevelGroup>();
            Console.WriteLine("TES4 Record:\n" + TES4);
            Console.WriteLine("Groups: \n");
            while (!esmbr.EndOfFile)
            {
                var group = esmbr.ReadTopLevel();
                topLevelGroups.Add(group.Lable, group);
                Console.WriteLine(group);
            }
        }
    }
}