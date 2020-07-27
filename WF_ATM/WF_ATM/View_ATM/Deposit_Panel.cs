using System;
using System.Drawing;
using System.Windows.Forms;
using WF_ATM.Model_ATM.Exceptions;

namespace WF_ATM.View_ATM
{
    public partial class Deposit_Panel : Form
    {
        private int depositSum = 0;
        private TextBox tbDeposit = new TextBox();
        public Deposit_Panel()
        {
            InitializeComponent();
        }

        private void Deposit_Panel_Load(object sender, EventArgs e)
        {
            //Параметры формы "Внесение наличных"
            this.Text = "Внесение наличных";
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
            lblHint.Text = "Вставьте деньги пачкой и нажмите ПРОДОЛЖИТЬ";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            lblHint.AutoSize = false;
            lblHint.Height = this.Height / 26;
            lblHint.Dock = DockStyle.Top;
            lblHint.Font = new Font("Microsoft Sans Serif", 9);
            this.Controls.Add(lblHint);

            //Параметры компонента надпись "Диалог"
            Label lblDialog = new Label();
            lblDialog.Text = "\nВведите сумму внесения";
            lblDialog.TextAlign = ContentAlignment.MiddleCenter;
            lblDialog.AutoSize = false;
            lblDialog.Height = this.Height / 4;
            lblDialog.Dock = DockStyle.Top;
            lblDialog.Top += 20;
            lblDialog.Font = new Font("Microsoft Sans Serif", 15);
            this.Controls.Add(lblDialog);

            //Параметры компонента ячейка "Сумма внесения"
            tbDeposit.AutoSize = false;
            tbDeposit.Width = this.Width / 2;
            tbDeposit.Height = this.Height / 12;
            tbDeposit.Top = this.Height / 3 + tbDeposit.Height;
            tbDeposit.Left = this.Width / 4;
            tbDeposit.CharacterCasing = CharacterCasing.Upper;
            tbDeposit.Font = new Font("Microsoft Sans Serif", 15);
            tbDeposit.MaxLength = 6;
            tbDeposit.TextAlign = HorizontalAlignment.Center;
            tbDeposit.KeyPress += tbDeposit_KeyPress;
            this.Controls.Add(tbDeposit);

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
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbDeposit.Text == "")
                    throw new DepositException("DepositException: Не указана сумма внесения наличных");
                depositSum = int.Parse(tbDeposit.Text);
                if (depositSum <= 0)
                    throw new DepositException("DepositException: Неверная сумма внесения наличных");
                MessageBox.Show(Program.ATM_Kernel_1.Deposit(depositSum), "Внесение наличных", MessageBoxButtons.OK);
                Finish_Panel Finish_Panel_1 = new Finish_Panel();
                Finish_Panel_1.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Операция отклонена! \n\nНекорректная сумма! \n\nЗаберите деньги и попробуйте снова.", "Некорректная сумма", MessageBoxButtons.OK);
            }
        }
        private void tbDeposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
