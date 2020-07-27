using System;
using System.Drawing;
using System.Windows.Forms;
using WF_ATM.View_ATM;

namespace WF_ATM
{
    public partial class Main_Panel : Form
    {
        public Main_Panel()
        {
            InitializeComponent();
        }
        private void Main_Panel_Load(object sender, EventArgs e)
        {
            //Параметры формы "Главное меню"
            this.Text = "Главное меню";
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

            //Параметры компонента надпись "Диалог"
            Label lblDialog = new Label();
            lblDialog.Text = "Выберите действие";
            lblDialog.TextAlign = ContentAlignment.MiddleCenter;
            lblDialog.AutoSize = false;
            lblDialog.Height = this.Height / 5;
            lblDialog.Dock = DockStyle.Top;
            lblDialog.Top += 20;
            lblDialog.Font = new Font("Microsoft Sans Serif", 15);
            this.Controls.Add(lblDialog);

            //Параметры компонента кнопка "Выход"
            Button btnExit = new Button();
            btnExit.Text = "ВЫХОД";
            btnExit.TextAlign = ContentAlignment.MiddleCenter;
            btnExit.AutoSize = false;
            btnExit.Width = this.Width - 40;
            btnExit.Height = this.Height / 6;
            btnExit.Top = this.Height - btnExit.Height - 50;
            btnExit.Left = 10;
            btnExit.Click += btnExit_Click;
            this.Controls.Add(btnExit);

            //Параметры компонента кнопка "Проверка состояния"
            Button btnStatus = new Button();
            btnStatus.Text = "ПРОВЕРКА СОСТОЯНИЯ";
            btnStatus.TextAlign = ContentAlignment.MiddleCenter;
            btnStatus.AutoSize = false;
            btnStatus.Width = this.Width - 40;
            btnStatus.Height = this.Height / 6;
            btnStatus.Top = this.Height - btnStatus.Height * 2 - 50;
            btnStatus.Left = 10;
            btnStatus.Click += btnStatus_Click;
            this.Controls.Add(btnStatus);

            //Параметры компонента кнопка "Внести наличные"
            Button btnDeposit = new Button();
            btnDeposit.Text = "ВНЕСТИ НАЛИЧНЫЕ";
            btnDeposit.TextAlign = ContentAlignment.MiddleCenter;
            btnDeposit.AutoSize = false;
            btnDeposit.Width = this.Width - 40;
            btnDeposit.Height = this.Height / 6;
            btnDeposit.Top = this.Height - btnDeposit.Height * 3 - 50;
            btnDeposit.Left = 10;
            btnDeposit.Click += btnDeposit_Click;
            this.Controls.Add(btnDeposit);

            //Параметры компонента кнопка "Снять наличные"
            Button btnWithdraw = new Button();
            btnWithdraw.Text = "СНЯТЬ НАЛИЧНЫЕ";
            btnWithdraw.TextAlign = ContentAlignment.MiddleCenter;
            btnWithdraw.AutoSize = false;
            btnWithdraw.Width = this.Width - 40;
            btnWithdraw.Height = this.Height / 6;
            btnWithdraw.Top = this.Height - btnWithdraw.Height * 4 - 50;
            btnWithdraw.Left = 10;
            btnWithdraw.Click += btnWithdraw_Click;
            this.Controls.Add(btnWithdraw);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.ATM_Help_1.Show(), "Справочная информация", MessageBoxButtons.OK);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void btnStatus_Click(object sender, EventArgs e)
        {
            Status_Panel Status_Panel_1 = new Status_Panel();
            this.Hide();
            Status_Panel_1.ShowDialog();
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Deposit_Panel Deposit_Panel_1 = new Deposit_Panel();
            this.Hide();
            Deposit_Panel_1.ShowDialog();
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Withdraw_Panel Withdraw_Panel_1 = new Withdraw_Panel();
            this.Hide();
            Withdraw_Panel_1.ShowDialog();
        }
    }
}
