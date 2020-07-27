using System;
using System.Windows.Forms;
using WF_ATM.Model_ATM;

namespace WF_ATM
{
    static class Program
    {
        public static Main_Panel Main_Panel_1;
        public static ATM_Help ATM_Help_1 = new ATM_Help();

        //Создание экземпляра настроек Банкомата
        private static int[] denominations = { 10, 50, 100, 500, 1000, 2000, 5000 };
        private static int transactionLimit = 200000;
        private static int storageLimit = 100;
        private static int collectionBoxLimit = 1000;

        //Создание экземпляра настроек Банкомата
        public static ATM_Settings ATM_Settings_1 = new ATM_Settings(denominations, transactionLimit, storageLimit, collectionBoxLimit);

        //Создание экземпляра ядра Банкомата
        public static ATM_Kernel ATM_Kernel_1 = new ATM_Kernel(ATM_Settings_1);

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Создание экземпляра ядра Банкомата
            //ATM_Kernel ATM_Kernel_1 = new ATM_Kernel(ATM_Settings_1);

            ATM_Kernel_1.RndLoad();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_Panel());
        }
    }
}
