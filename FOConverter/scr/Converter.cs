using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using FOConverter.scr.F3;
using FOConverter.scr.Groups;

namespace FOConverter.scr
{
    public class Converter
    {
        private EsmDatabase esmBase;

        private EsmDatabase getEsmAdapter()
        {
            return new F3EsmDatabase();
//            throw new Exception("unknown converter type");
        }

        public void Process(Configuration configData)
        {
            esmBase = getEsmAdapter();
            esmBase.Read(configData.Fallout3DataPath);
        }
    }
}