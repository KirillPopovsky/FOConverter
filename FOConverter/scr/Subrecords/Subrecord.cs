namespace FOConverter.scr.Subrecords
{
    public abstract class Subrecord
    {
        private byte[] data;
        public abstract byte[] GetF3Data();
        public abstract byte[] GetFNVData();
        public abstract byte[] GetF4Data();
        public abstract string Head { get; }
    }
}