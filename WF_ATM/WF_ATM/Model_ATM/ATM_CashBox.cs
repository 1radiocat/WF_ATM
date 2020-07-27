using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_ATM.Model_ATM
{
    /// <summary>
    /// Базовый класс кассеты с купюрами Банкомата.
    /// </summary>
    class ATM_CashBox
    {
        private readonly int denomination; //Номинал кассеты
        private int count; //Текущее количество купюр в кассете
        private readonly int storageLimit; //Максимальное количество купюр в кассете

        public ATM_CashBox(int denomination, int count, int storageLimit)
        {
            this.denomination = denomination;
            this.count = count;
            this.storageLimit = storageLimit;
        }

        public int Denomination
        {
            get { return denomination; }
        }

        public int Count
        {
            get { return count; }
            set { this.count = value; }
        }

        public int StorageLimit
        {
            get { return storageLimit; }
        }
    }
}
