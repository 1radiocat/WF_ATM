using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_ATM.Model_ATM.Exceptions
{
    public static class LogEx
    {
        public static string outputPath = AppDomain.CurrentDomain.BaseDirectory + "LogEx.txt";
        public static StreamWriter sw;

        public static void Record(string message)
        {
            try
            {
                sw = new StreamWriter(outputPath, true);
                sw.WriteLine(DateTime.Now.ToString() + " " + message);
            }
            catch
            {

            }
            finally
            {
                sw.Close();
            }
        }
    }
}
