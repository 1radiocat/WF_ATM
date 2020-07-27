using System.IO;


namespace WF_ATM.Model_ATM
{
    /// <summary>
    /// Базовый класс документации по Банкомату.
    /// </summary>
    public class ATM_Help
    {
        public string Show()
        {
            string inputHelp = "readme.hlp";

            try
            {
                using (StreamReader sr = new StreamReader(inputHelp))
                    return sr.ReadToEnd();
            }
            catch
            {
                return "Ошибка! Файл справки \"readme.hlp\" не найден в корневом каталоге программы или повреджён!";
            }
        }
    }
}