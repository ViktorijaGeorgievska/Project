using System;
using System.Windows.Forms;

namespace Project
{
    public class StartForm : Form
    {
        public StartForm()
        {
            // Центрирај ја формата
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Box Pusher - Мени";
            this.Width = 400;
            this.Height = 300;

            // Наслов
            Label lblTitle = new Label();
            lblTitle.Text = "BOX PUSHER";
            lblTitle.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Top = 30;
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitle);

            // Копче - Започни игра
            Button btnStart = new Button();
            btnStart.Text = "Започни игра";
            btnStart.Width = 150;
            btnStart.Height = 40;
            btnStart.Top = 100;
            btnStart.Left = (this.ClientSize.Width - btnStart.Width) / 2;
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            // Копче - Како да се игра
            Button btnInstructions = new Button();
            btnInstructions.Text = "Како да се игра";
            btnInstructions.Width = 150;
            btnInstructions.Height = 40;
            btnInstructions.Top = 150;
            btnInstructions.Left = (this.ClientSize.Width - btnInstructions.Width) / 2;
            btnInstructions.Click += BtnInstructions_Click;
            this.Controls.Add(btnInstructions);

            // Копче - Излез
            Button btnExit = new Button();
            btnExit.Text = "Излез";
            btnExit.Width = 150;
            btnExit.Height = 40;
            btnExit.Top = 200;
            btnExit.Left = (this.ClientSize.Width - btnExit.Width) / 2;
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Сокриј го менито и стартувај ја играта
            this.Hide();
            Form1 gameForm = new Form1();
            gameForm.StartPosition = FormStartPosition.CenterScreen;
            gameForm.ShowDialog();
            this.Show();
        }

        private void BtnInstructions_Click(object sender, EventArgs e)
        {
            string instructions = "🎮 Како да се игра:\n\n" +
                                  "1️⃣ Користи стрелки на тастатурата за движење.\n" +
                                  "2️⃣ Турни ја кутијата (кафена) кон целта (жолта).\n" +
                                  "3️⃣ Планирај потезите за да не ја заглавиш кутијата во ќош.\n" +
                                  "4️⃣ Кутијата мора да ја стигне целта за да победиш.\n\n" +
                                  "💡 Совет: Размислувај неколку потези однапред!";
            MessageBox.Show(instructions, "Инструкции", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
