using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace FOConverter.scr
{
    public abstract class EsmBaseAdapter
    {
        public abstract void Read(string _path);
    }


    public class F3baseAdaper : EsmBaseAdapter
    {
        public override void Read(string _path)
        {
            var esmbr = new EsmBinaryReader(_path + "\\Fallout3.esm");
            var r = esmbr.ReadRecordHeader();
            Console.WriteLine(r);
        }
    }

    public class Converter
    {
        private EsmBaseAdapter esmBase;

        private EsmBaseAdapter getEsmAdapter(string type)
        {
            switch (type)
            {
                case Constants.F3ToF4:
                    return new F3baseAdaper();
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