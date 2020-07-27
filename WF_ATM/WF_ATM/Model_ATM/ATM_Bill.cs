using System;
using System.IO;

namespace WF_ATM.Model_ATM
{
    static class ATM_Bill
    {
        public static string outputPath = AppDomain.CurrentDomain.BaseDirectory + "CashRegTape.txt";
        public static StreamWriter sw;
        private static string bill;

        public static string Record()
        {
            try
            {
                sw = new StreamWriter(outputPath, true);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(bill);
                sw.WriteLine("-------------------------------------------");
                return bill;
            }
            catch
            {
                return "Ошибка при печати чека!";
            }
            finally
            {
                sw.Close();
            }
        }
        public static string Print()
        {
            string result = bill;
            result = String.Concat("-------------------------------------------\n", result);
            result = String.Concat("Дата операции: " + DateTime.Now.ToString() + " \n", result);
            result = String.Concat("-------------------------------------------\n", result);
            result = String.Concat(result, "\n-------------------------------------------");
            return result;
        }
        public static string Billing
        {
            get { return bill; }
            set { bill = value; }
        }
    }
}
