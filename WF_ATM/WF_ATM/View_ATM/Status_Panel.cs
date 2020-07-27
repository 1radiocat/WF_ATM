using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF_ATM.View_ATM
{
    public partial class Status_Panel : Form
    {
        public Label lblDialogStatus = new Label();
        public Status_Panel()
        {
            InitializeComponent();
        }

        private void Status_Panel_Load(object sender, EventArgs e)
        {
            //Параметры формы "Панель состояния"
            this.Text = "Панель состояния";
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
            lblDialog.Text = "\nСостояние банкомата\n\nВ банкомат загружены следующие купюры";
            lblDialog.TextAlign = ContentAlignment.MiddleCenter;
            lblDialog.AutoSize = false;
            lblDialog.Height = this.Height / 4;
            lblDialog.Dock = DockStyle.Top;
            lblDialog.Top += 20;
            lblDialog.Font = new Font("Microsoft Sans Serif", 15);
            this.Controls.Add(lblDialog);

            //Параметры компонента надпись "Диалог состояния"
            lblDialogStatus.Text = "<null>";
            lblDialogStatus.AutoSize = true;
            lblDialogStatus.Top = this.Height - this.Height / 5 * 3 - 30;
            lblDialogStatus.Left = 20;
            lblDialogStatus.Font = new Font("Microsoft Sans Serif", 11);
            this.Controls.Add(lblDialogStatus);

            //Обновление состояния
            RefreshCashBox();

            //Параметры компонента панель "Состояние"
            Panel pnlStatus = new Panel();
            pnlStatus.BorderStyle = BorderStyle.FixedSingle;
            pnlStatus.AutoSize = false;
            pnlStatus.Width = this.Width - 40;
            pnlStatus.Height = this.Height / 5;
            pnlStatus.Top = this.Height - pnlStatus.Height * 3 - 40;
            pnlStatus.Left = 10;
            this.Controls.Add(pnlStatus);

            //Параметры компонента кнопка "Загрузить банкомат случайными купюрами"
            Button btnRndLoad = new Button();
            btnRndLoad.Text = "ЗАГРУЗИТЬ БАНКОМАТ СЛУЧАЙНЫМИ КУПЮРАМИ";
            btnRndLoad.TextAlign = ContentAlignment.MiddleCenter;
            btnRndLoad.AutoSize = false;
            btnRndLoad.Width = this.Width - 40;
            btnRndLoad.Height = this.Height / 6;
            btnRndLoad.Top = this.Height - btnRndLoad.Height * 2 - 50;
            btnRndLoad.Left = 10;
            btnRndLoad.Click += btnRndLoad_Click;
            this.Controls.Add(btnRndLoad);

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

        private void RefreshCashBox()
        {
            lblDialogStatus.Text = null;    
            for (int i = 0; i < Program.ATM_Kernel_1.CashBoxCount; i++)
            {
                lblDialogStatus.Text += Program.ATM_Kernel_1.CashBoxStatus[i].Denomination.ToString() + "р - " + Program.ATM_Kernel_1.CashBoxStatus[i].Count.ToString() + "шт  ";
            }
            lblDialogStatus.Text += "\n\nВ ящике для инкассации доступно " + Program.ATM_Kernel_1.CollectionBoxStatus + " мест для купюр.";
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.ATM_Help_1.Show(), "Справочная информация", MessageBoxButtons.OK);
        }
        private void btnRndLoad_Click(object sender, EventArgs e)
        {
            Program.ATM_Kernel_1.RndLoad();
            RefreshCashBox();
        }
        private void btnBack_Click(object sender, EventArgs e)
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
