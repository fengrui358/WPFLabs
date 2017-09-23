using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLabs.Helper
{
    public static class DebugWriterHelper
    {


        public static void WriterLine(string msg)
        {
            Debug.WriteLine($"{DateTime.Now:yyyyMMdd-HH:mm:ss}-【{msg}】");
        }
    }
}
