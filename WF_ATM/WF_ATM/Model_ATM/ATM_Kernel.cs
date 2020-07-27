using System;
using System.Collections.Generic;
using WF_ATM.Model_ATM.Exceptions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace WF_ATM.Model_ATM
{
    /// <summary>
    /// Базовый класс ядра Банкомата.
    /// </summary>
    public class ATM_Kernel
    {
        private readonly ATM_Settings settings;
        private List<ATM_CashBox> listCashBox = new List<ATM_CashBox>();
        private int collectionBox;

        public ATM_Kernel(ATM_Settings settings)
        {
            this.settings = settings;
        }

        //Метод загрузки Банкомата случайными купюрами
        public void RndLoad()
        {
            listCashBox.Clear();
            Random rnd = new Random();

            for (int i = 0; i < this.settings.Denominations.Count; i++)
            {
                ATM_CashBox tmpCashBox = new ATM_CashBox(settings.Denominations[i], rnd.Next(0, settings.StorageLimit), settings.StorageLimit);
                listCashBox.Add(tmpCashBox);
                collectionBox = settings.CollectionBoxLimit - rnd.Next(0, settings.CollectionBoxLimit);
            }
        }

        //Метод внесения наличных в Банкомат (случайный парсинг на купюры)
        public string Deposit(int depositSum)
        {
            int divCount = 0;
            int item, over;
            int tmpSum = depositSum;
            int[,] cashCount = new int[listCashBox.Count, 2];
            bool successDeposit = true;
            ATM_Bill.Billing = "";


            //Сумма должна быть кратной хотябы одному из номиналов банкнот
            for (int i = 0; i < listCashBox.Count; i++)
                if ((listCashBox[i].Denomination > 0) && (depositSum % listCashBox[i].Denomination == 0))
                    divCount ++;

            if (divCount > 0)
            {
                //Случайное разделение внесенной суммы на банкноты
                Random rnd = new Random();
                do
                {
                    item = rnd.Next(0, listCashBox.Count);
                    if (tmpSum >= listCashBox[item].Denomination)
                    {
                        tmpSum -= listCashBox[item].Denomination;
                        cashCount[item, 0] = listCashBox[item].Denomination;
                        cashCount[item, 1] += 1;
                    }
                }
                while (tmpSum != 0);

                //Резервирование данных для восстановления в случае неуспешной операции
                List<ATM_CashBox> backupCashBox = new List<ATM_CashBox>();
                int backupCollectionBox = 0;
                for (int i = 0; i < settings.Denominations.Count; i++)
                {
                    ATM_CashBox tmpCashBox = new ATM_CashBox(settings.Denominations[i], listCashBox[i].Count, settings.StorageLimit);
                    backupCashBox.Add(tmpCashBox);
                }
                backupCollectionBox = collectionBox;

                //Укладака банкнот в кассеты и контейнер для инкассации
                for (int i = 0; i < listCashBox.Count; i++)
                    for (int j = 0; j < cashCount.GetLength(0); j++)
                    {
                        if (listCashBox[i].Denomination == cashCount[j, 0])
                        {
                            if ((listCashBox[i].Count + cashCount[j, 1]) <= listCashBox[i].StorageLimit)
                                listCashBox[i].Count += cashCount[j, 1];
                            else if ((listCashBox[i].Count + cashCount[j, 1]) > listCashBox[i].StorageLimit)
                            {
                                if ((listCashBox[i].Count < listCashBox[i].StorageLimit) && (collectionBox > 0))
                                {
                                    over = Math.Abs(listCashBox[i].StorageLimit - (listCashBox[i].Count + cashCount[j, 1]));
                                    if ((collectionBox - over) > 0)
                                    {
                                        listCashBox[i].Count = listCashBox[i].StorageLimit;
                                        collectionBox -= over;
                                    }
                                    else
                                        successDeposit = false;
                                }
                                else if ((listCashBox[i].Count == listCashBox[i].StorageLimit) && (collectionBox > 0))
                                {
                                    if ((collectionBox - cashCount[j, 1]) > 0)
                                        collectionBox -= cashCount[j, 1];
                                    else
                                        successDeposit = false;
                                }
                            }
                        }
                    }

                //Сохранение, выдача и чек
                if (successDeposit)
                {
                    ATM_Bill.Billing += "Внесена сумма: \n\n" + depositSum + " руб 00 коп \n\nПриняты купюры:\n ";
                    for (int j = listCashBox.Count - 1; j >= 0; j--)
                    {
                        if (cashCount[j, 0] > 0)
                        {
                            ATM_Bill.Billing += cashCount[j, 0] + " руб = " + cashCount[j, 1] + " шт\n ";
                        }
                    }
                }
                else
                {
                    listCashBox = backupCashBox;
                    collectionBox = backupCollectionBox;
                    foreach (var x in listCashBox)
                        Console.WriteLine(x.Denomination + " = " + x.Count);
                    ATM_Bill.Billing += "Операция отклонена! \n\nК сожалению, наш ящик для инкассации \nне может вместить такое количество купюр. \nПожалуйста, уменьшите количество и повторите операцию.";
                }
            }             
            else
            {
                ATM_Bill.Billing += "Операция отклонена! \n\nК сожалению, наш банкомат не поддерживает купюры данного номинала. \n\nНевозможно внести сумму: \n\n" + depositSum + " руб 00 коп \n\nИспользуйте купюры другого номинала и повторите операцию.";
            }

            ATM_Bill.Billing += " \nСпасибо, что выбрали наш банкомат!";
            return ATM_Bill.Record();
        }

        //Метод проверки корректности суммы перед выдачей наличных
        public bool CheckSum(int withdrawSum, bool[] whatDenomination)
        {
            int min = int.MaxValue;
            int[] tmpDenomination = new int[settings.Denominations.Count];
            tmpDenomination = settings.Denominations.ToArray();
            //Парсинг выбранных купюр и выбор минимальной
            for (int i = 0; i < tmpDenomination.Length; i++)
            {
                if (!whatDenomination[i]) tmpDenomination[i] = 0;
                if ((min > tmpDenomination[i]) && (tmpDenomination[i] != 0)) min = tmpDenomination[i];
            }
            //Если сумма к выдаче меньше минимальной выбранной купюры, 0 или меньше, то False
            if ((withdrawSum < min) || (withdrawSum <= 0)) return false;
            else return true;
        }

        //Метод выдачи наличных
        public string Withdraw(int withdrawSum, bool[] whatDenomination)
        {
            int balance = withdrawSum;
            int[,] denominationSelected = new int[settings.Denominations.Count, 2];
            bool withdrawSuccess = false;
            int[,] billingReport = new int[denominationSelected.GetLength(0), 2];
            ATM_Bill.Billing = "";

            //Построение массива выбранных купюр с доступным количеством
            for (int i = 0; i < denominationSelected.GetLength(0); i++)
            {
                if ((whatDenomination[i]) && (listCashBox[i].Count > 0))
                {
                    denominationSelected[i, 0] = listCashBox[i].Denomination;
                    denominationSelected[i, 1] = listCashBox[i].Count;
                }
                else
                {
                    denominationSelected[i, 0] = 0;
                    denominationSelected[i, 1] = 0;
                }
            }

            //Выдача наличных начиная с крупных
            List<ATM_CashBox> withdrawCashBox = new List<ATM_CashBox>();
            for (int i = 0; i < settings.Denominations.Count; i++)
            {
                ATM_CashBox tmpCashBox = new ATM_CashBox(settings.Denominations[i], listCashBox[i].Count, settings.StorageLimit);
                withdrawCashBox.Add(tmpCashBox);
            }
            for (int i = denominationSelected.GetLength(0) - 1; i >= 0; i--)
            {
                if ((balance > 0) && (denominationSelected[i, 0] > 0) 
                    && ((withdrawCashBox[i].Count - balance / denominationSelected[i, 0]) >= 0) 
                    && ((balance / denominationSelected[i, 0]) > 0))
                {
                    withdrawCashBox[i].Count -= balance / denominationSelected[i, 0];
                    billingReport[i, 0] = withdrawCashBox[i].Denomination;
                    billingReport[i, 1] += balance / denominationSelected[i, 0];
                    balance = balance % denominationSelected[i, 0];
                }
            }

            //Если выбранных купюр оказалось недостаточно, выдача производится имеющимися
            if (balance > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Указанная сумма не может быть выдана выбранными купюрами! \n\nБанкомат автоматически подберет необходимые купюры. \n\n Хотите продолжить операцию и получить наличные?", "Продолжение операции", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = denominationSelected.GetLength(0) - 1; i >= 0; i--)
                    {
                        if ((balance > 0) && (withdrawCashBox[i].Count > 0)
                            && ((withdrawCashBox[i].Count - balance / withdrawCashBox[i].Denomination) >= 0)
                            && ((balance / withdrawCashBox[i].Denomination) > 0))
                        {
                            withdrawCashBox[i].Count -= balance / withdrawCashBox[i].Denomination;
                            billingReport[i, 0] = withdrawCashBox[i].Denomination;
                            billingReport[i, 1] += balance / withdrawCashBox[i].Denomination;
                            balance = balance % withdrawCashBox[i].Denomination;
                        }
                    }
                    withdrawSuccess = true;

                    //Сообщение об успешной операции с вмешательством ИИ банкомата
                    if ((balance == 0) && (withdrawSuccess))
                        ATM_Bill.Billing += ("Вы выбрапли купюры, неподходящие для выдачи запрашиваемой суммы.\nНо мы всё предусмотрели и банкомат доложил необходимые банкноты :)\n\n");
                }
            }

            //Отмена операции, если не получилось выдать всё
            if (balance > 0)
            {
                ATM_Bill.Billing += "Сумма к выдаче: \n\n" + withdrawSum + " руб 00 коп\n\n";
                ATM_Bill.Billing += "Ошибка! Остаток: " + balance.ToString() + " руб не может быть выдан! Операция отклонена!";
                for (int i = denominationSelected.GetLength(0) - 1; i >= 0; i--)
                    withdrawCashBox[i].Count += billingReport[i, 1];
            }
            else
            {
                //Сохранение, выдача и чек
                listCashBox = withdrawCashBox;

                ATM_Bill.Billing += "Сумма к выдаче: \n\n" + withdrawSum + " руб 00 коп\n\n";
                ATM_Bill.Billing += "Выдано: \n";
                for (int i = denominationSelected.GetLength(0) - 1; i >= 0; i--)
                {
                    if (billingReport[i, 0] > 0)
                    {
                        ATM_Bill.Billing += billingReport[i, 0] + " руб = " + billingReport[i, 1] + " шт \n";
                    }
                }
            }
            ATM_Bill.Billing += " \nСпасибо, что выбрали наш банкомат!";
            return ATM_Bill.Record();
        }

        //Метод мониторинга количества кассет в Банкомате
        public int CashBoxCount
        {
            get { return this.listCashBox.Count; }
        }

        //Метод мониторинга и изменения состояния кассет Банкомата
        public dynamic CashBoxStatus
        {
            get { return this.listCashBox; }
            set { this.listCashBox = value; }
        }

        //Метод мониторинга и изменения состояния ящика для инкассации Банкомата
        public int CollectionBoxStatus
        {
            get { return this.collectionBox; }
            set { this.collectionBox = value; }
        }
    }
}
