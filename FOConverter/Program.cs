using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOConverter.scr;

namespace FOConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration config = new Configuration();
            config.ReadConfig();
        }
    }
}