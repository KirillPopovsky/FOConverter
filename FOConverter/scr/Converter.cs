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

        private EsmDatabase getEsmAdapter(string type)
        {
            switch (type)
            {
                case Constants.F3ToF4:
                    return new F3EsmDatabase();
                    break;
            }

            throw new Exception("unknown converter type");
        }

        public void Process(PathsPropsConfig configData)
        {
            esmBase = getEsmAdapter(configData.ConvertType);
            esmBase.Read(configData.SourceDataPath);
        }
    }
}