using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WF_ATM.Model_ATM;
using WF_ATM.Model_ATM.Exceptions;

namespace WF_ATM.View_ATM
{
    public partial class Withdraw_Panel : Form
    {
        private int withdrawSum = 0;
        private TextBox tbWithdraw = new TextBox();
        private bool[] whatDenomination = new bool[Program.ATM_Kernel_1.CashBoxCount];
        private Panel pnlDenominations = new Panel();
        public Withdraw_Panel()
        {
            InitializeComponent();
        }

        private void Withdraw_Panel_Load(object sender, EventArgs e)
        {
            //Параметры формы "Снятие наличных"
            this.Text = "Снятие наличных";
            this.Width = 700;
            this.Height = 400;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;

            //Параметры компонента кнопка "Справка"
            Button btnHelp = new Button();
            btnHelp.Text = "СПРАВКА";
            btnHelp.TextAlign = ContentAlignment.MiddleCenter;
            btnHelp.AutoSize = false;
            btnHelp.Width = this.Width / 7;
            btnHelp.Height = this.Height / 8;
            btnHelp.Top = 10;
            btnHelp.Left = this.Width - btnHelp.Width - 30;
            btnHelp.BringToFront();
            btnHelp.Click += btnHelp_Click;
            this.Controls.Add(btnHelp);

            //Параметры компонента надпись "Подсказка"
            Label lblHint = new Label();
            lblHint.Text = "Сумма не может быть более " + Program.ATM_Settings_1.TransactionLimit + " рублей за одну транзакцию";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            lblHint.AutoSize = false;
            lblHint.Height = this.Height / 26;
            lblHint.Dock = DockStyle.Top;
            lblHint.Font = new Font("Microsoft Sans Serif", 9);
            this.Controls.Add(lblHint);

            //Параметры компонента надпись "Диалог"
            Label lblDialog = new Label();
            lblDialog.Text = "\nВведите сумму снятия";
            lblDialog.TextAlign = ContentAlignment.MiddleCenter;
            lblDialog.AutoSize = false;
            lblDialog.Height = this.Height / 5;
            lblDialog.Dock = DockStyle.Top;
            lblDialog.Top += 20;
            lblDialog.Font = new Font("Microsoft Sans Serif", 15);
            this.Controls.Add(lblDialog);

            //Параметры компонента ячейка "Сумма снятия"
            tbWithdraw.AutoSize = false;
            tbWithdraw.Width = this.Width / 2;
            tbWithdraw.Height = this.Height / 12;
            tbWithdraw.Top = this.Height / 4 + tbWithdraw.Height - 20;
            tbWithdraw.Left = this.Width / 4;
            tbWithdraw.CharacterCasing = CharacterCasing.Upper;
            tbWithdraw.Font = new Font("Microsoft Sans Serif", 15);
            tbWithdraw.MaxLength = 6;
            tbWithdraw.TextAlign = HorizontalAlignment.Center;
            tbWithdraw.KeyPress += tbWithdraw_KeyPress;
            this.Controls.Add(tbWithdraw);

            //Динамическое создание компонентов выбора купюр
            List<CheckBox> cbList = new List<CheckBox>();
            for (int i = 0; i < Program.ATM_Kernel_1.CashBoxCount; i++)
            {
                CheckBox tmpCheckBox = new CheckBox();
                tmpCheckBox.Name = "cb" + Program.ATM_Settings_1.Denominations[i];
                tmpCheckBox.Text = Program.ATM_Settings_1.Denominations[i].ToString();
                tmpCheckBox.Font = new Font("Microsoft Sans Serif", 12);
                tmpCheckBox.Width = 85;
                tmpCheckBox.Top = this.Height / 2 + this.Height / 18;
                tmpCheckBox.Tag = i;
                //Размещение компонентов в один ряд
                if (i >= 1)
                    tmpCheckBox.Left = cbList[i-1].Left + tmpCheckBox.Width;
                else
                    tmpCheckBox.Left = 40;
                //Проверка наличия купюр с номиналами в кассетах
                if (Program.ATM_Kernel_1.CashBoxStatus[i].Count <= 0)
                    tmpCheckBox.Enabled = false;
                else
                    tmpCheckBox.Enabled = true;
                cbList.Add(tmpCheckBox);
                this.Controls.Add(tmpCheckBox);
            }

            //Параметры компонента панель "Выбор купюр"
            pnlDenominations.BorderStyle = BorderStyle.FixedSingle;
            pnlDenominations.AutoSize = false;
            pnlDenominations.Width = this.Width - 40;
            pnlDenominations.Height = this.Height / 6;
            pnlDenominations.Top = this.Height / 2;
            pnlDenominations.Left = 10;
            this.Controls.Add(pnlDenominations);

            //Параметры компонента надпись "Подсказка панели"
            Label lblHintPanel = new Label();
            lblHintPanel.Text = "Выберите купюры доступного номинала";
            lblHintPanel.TextAlign = ContentAlignment.MiddleCenter;
            lblHintPanel.AutoSize = false;
            lblHintPanel.Width = this.Width - 40;
            lblHintPanel.Top = pnlDenominations.Top - lblHintPanel.Height - 10;
            lblHintPanel.Left = 10;
            lblHintPanel.Font = new Font("Microsoft Sans Serif", 12);
            this.Controls.Add(lblHintPanel);

            //Параметры компонента кнопка "Назад"
            Button btnBack = new Button();
            btnBack.Text = "НАЗАД";
            btnBack.TextAlign = ContentAlignment.MiddleCenter;
            btnBack.AutoSize = false;
            btnBack.Width = this.Width / 2 - 10;
            btnBack.Height = this.Height / 6;
            btnBack.Top = this.Height - btnBack.Height - 50;
            btnBack.Left = 10;
            btnBack.Click += btnBack_Click;
            this.Controls.Add(btnBack);

            //Параметры компонента кнопка "Продолжить"
            Button btnNext = new Button();
            btnNext.Text = "ПРОДОЛЖИТЬ";
            btnNext.TextAlign = ContentAlignment.MiddleCenter;
            btnNext.AutoSize = false;
            btnNext.Width = this.Width / 2 - 30;
            btnNext.Height = this.Height / 6;
            btnNext.Top = this.Height - btnNext.Height - 50;
            btnNext.Left = this.Width / 2;
            btnNext.Click += btnNext_Click;
            this.Controls.Add(btnNext);
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.ATM_Help_1.Show(), "Справочная информация", MessageBoxButtons.OK);
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Program.Main_Panel_1 = new Main_Panel();
            Program.Main_Panel_1.Show();
            this.Close();
        }
        private void tbWithdraw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            bool success = false;
            //Получение и проверка суммы снятия
            try
            {
                withdrawSum = int.Parse(tbWithdraw.Text);
                success = true;
            }
            catch
            {
                MessageBox.Show("Операция отклонена! \n\nНекорректная сумма к выдаче! \n\nВведите другую сумму и нажмите \"Продолжить\".", "Некорректная сумма", MessageBoxButtons.OK);
            }

            if (success)
            {
                int selectCount = 0;
                //Проверка выбранных номиналов купюр
                foreach (CheckBox checkBox in this.Controls.OfType<CheckBox>())
                {
                    {
                        if (checkBox.Checked)
                        {
                            whatDenomination[Convert.ToInt32(checkBox.Tag)] = true;
                            selectCount ++;
                        }
                        else if (!checkBox.Checked)
                            whatDenomination[Convert.ToInt32(checkBox.Tag)] = false;
                    }
                }
                //Проверка лимитов за одну транзакцию
                try
                {
                    if (withdrawSum > Program.ATM_Settings_1.TransactionLimit)
                        throw new WithdrawException("WithdrawException: Сумма снятия превышает лимит одной транзакции: " + Program.ATM_Settings_1.TransactionLimit + " руб");
                    else if (selectCount <= 0)
                        MessageBox.Show("Не выбраны купюры! Выберите купюры нужного номинала и нажмите \"Продолжить\".", "Не выбраны купюры", MessageBoxButtons.OK);
                    else if (!Program.ATM_Kernel_1.CheckSum(withdrawSum, whatDenomination))
                        MessageBox.Show("Указана некорректная сумма или сумма не соответсвует выбранным купюрам! Введите корректную сумму и нажмите \"Продолжить\".", "Некорректная сумма", MessageBoxButtons.OK);
                    else
                    {
                        //Запуск выдачи, переход к следующему экрану
                        MessageBox.Show(Program.ATM_Kernel_1.Withdraw(withdrawSum, whatDenomination), "Выдача наличных", MessageBoxButtons.OK);
                        Finish_Panel Finish_Panel_1 = new Finish_Panel();
                        Finish_Panel_1.Show();
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Сумма снятия не может превышать " + Program.ATM_Settings_1.TransactionLimit + " рублей за одну операцию! Введите другую сумму и нажмите \"Продолжить\".", "Превышен лимит", MessageBoxButtons.OK);
                }
                

            }
        }
    }
}
