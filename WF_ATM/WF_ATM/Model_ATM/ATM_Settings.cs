using System;
using System.Collections.ObjectModel;
using WF_ATM.Model_ATM.Exceptions;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace WF_ATM.Model_ATM
{
    /// <summary>
    /// Базовый класс настроек Банкоматов.
    /// </summary>
    public class ATM_Settings
    {
        private readonly List<int> denominations = null; //Номиналы допустимых банкнот
        private readonly int transactionLimit; //Лимит одной операции снятия
        private readonly int storageLimit; //Лимит максимальной загрузки каждой кассеты с купюрами
        private readonly int collectionBoxLimit; //Лимит контейнера для инкассации (всё что не сортируется по кассетам попадает в этот контейнер)

        public ATM_Settings(int[] denominations, int transactionLimit, int storageLimit, int collectionBoxLimit)
        {
            try
            {
                if (denominations == null)
                    throw new SettingsException("SettingsException: Массив с номиналами банкнот пуст (null)");
                else
                    this.denominations = denominations.OfType<int>().ToList();

                if (transactionLimit <= 0)
                    throw new SettingsException("SettingsException: Нулевой или отрицательный лимит транзакций");
                else
                    this.transactionLimit = transactionLimit;

                if (storageLimit <= 0)
                    throw new SettingsException("SettingsException: Нулевой или отрицательный лимит загрузки кассет");
                else
                    this.storageLimit = storageLimit;

                if (collectionBoxLimit < 0)
                    throw new SettingsException("SettingsException: Отрицательный лимит ящика для инкассации");
                else
                    this.collectionBoxLimit = collectionBoxLimit;
            }
            catch
            {
                MessageBox.Show("Установлены неверные параметры Банкомата!\n\nПриложение будет завершено!\n\nПожалуйста, создайте верный экземпляр настроек ATM_Settings и повторите запуск.", "Ошибка настроек Банкомата: SettingsException", MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        public List<int> Denominations
        {
            get { return denominations; }
        }

        public int TransactionLimit
        {
            get { return transactionLimit; }
        }

        public int StorageLimit
        {
            get { return storageLimit; }
        }

        public int CollectionBoxLimit
        {
            get { return collectionBoxLimit; }
        }
    }
}
