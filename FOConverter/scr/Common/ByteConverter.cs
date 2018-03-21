using System.Linq;
using System.Text;

namespace FOConverter.scr.Common
{
    public class ByteConverter
    {
        public static string ByteArrayToHexString(byte[] _b)
        {
            var ba = _b.Reverse().ToArray();
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string ByteArrayToString(byte[] _b)
        {
            return System.Text.Encoding.Default.GetString(_b);
        }
    }
}