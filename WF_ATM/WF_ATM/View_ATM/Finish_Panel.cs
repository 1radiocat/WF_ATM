using System;
using System.Drawing;
using System.Windows.Forms;
using WF_ATM.Model_ATM;

namespace WF_ATM.View_ATM
{
    public partial class Finish_Panel : Form
    {
        public Finish_Panel()
        {
            InitializeComponent();
        }

        private void Finish_Panel_Load(object sender, EventArgs e)
        {
            //Параметры формы "Завершение операции"
            this.Text = "Завершение операции";
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
            lblDialog.Text = "\n\nСпасибо, что выбрали наш банк!";
            lblDialog.TextAlign = ContentAlignment.MiddleCenter;
            lblDialog.AutoSize = false;
            lblDialog.Height = this.Height / 4;
            lblDialog.Dock = DockStyle.Top;
            lblDialog.Top += 20;
            lblDialog.Font = new Font("Microsoft Sans Serif", 15);
            this.Controls.Add(lblDialog);

            //Параметры компонента кнопка "Напечатать чек"
            Button btnPrintBill = new Button();
            btnPrintBill.Text = "НАПЕЧАТАТЬ ЧЕК";
            btnPrintBill.TextAlign = ContentAlignment.MiddleCenter;
            btnPrintBill.AutoSize = false;
            btnPrintBill.Width = this.Width - 40;
            btnPrintBill.Height = this.Height / 6;
            btnPrintBill.Top = this.Height - btnPrintBill.Height * 3 - 50;
            btnPrintBill.Left = 10;
            btnPrintBill.Click += btnPrintBill_Click;
            this.Controls.Add(btnPrintBill);

            //Параметры компонента кнопка "В главное меню"
            Button btnMainMenu = new Button();
            btnMainMenu.Text = "В ГЛАВНОЕ МЕНЮ";
            btnMainMenu.TextAlign = ContentAlignment.MiddleCenter;
            btnMainMenu.AutoSize = false;
            btnMainMenu.Width = this.Width / 2 - 10;
            btnMainMenu.Height = this.Height / 6;
            btnMainMenu.Top = this.Height - btnMainMenu.Height - 50;
            btnMainMenu.Left = 10;
            btnMainMenu.Click += btnMainMenu_Click;
            this.Controls.Add(btnMainMenu);

            //Параметры компонента кнопка "Выход"
            Button btnExit = new Button();
            btnExit.Text = "ВЫХОД";
            btnExit.TextAlign = ContentAlignment.MiddleCenter;
            btnExit.AutoSize = false;
            btnExit.Width = this.Width / 2 - 30;
            btnExit.Height = this.Height / 6;
            btnExit.Top = this.Height - btnExit.Height - 50;
            btnExit.Left = this.Width / 2;
            btnExit.Click += btnExit_Click;
            this.Controls.Add(btnExit);
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.ATM_Help_1.Show(), "Справочная информация", MessageBoxButtons.OK);
        }
        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ATM_Bill.Print(), "Возьмите чек", MessageBoxButtons.OK);
        }
        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Program.Main_Panel_1 = new Main_Panel();
            Program.Main_Panel_1.Show();
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
