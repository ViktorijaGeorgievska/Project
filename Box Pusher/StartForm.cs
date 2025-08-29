using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            lblName.Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1();
            gameForm.StartPosition = FormStartPosition.CenterScreen;

            // кога ќе се затвори играта, апликацијата целосно ќе заврши
            gameForm.FormClosed += (s, args) => Application.Exit();

            gameForm.Show();
            this.Hide();       // скриј ја старт формата
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            string instructions = "🎮 Како да играш:\n\n" +
                      "1️⃣ Користи ги стрелките на тастатурата за движење.\n" +
                      "2️⃣ Кутијата треба да ја туркаш кон целта.\n" +
                      "3️⃣ Планирај ги потезите за да не ја заглавиш кутијата во агол.\n" +
                      "4️⃣ Кутијата мора да ја поставиш на целта за да успешно го завршиш нивото.\n" +
                      "5️⃣ По успешно завршување на нивото или заглавување на кутијата во агол, притисни ENTER.\n\n" +
                      "💡 Совет: Размислувај неколку потези однапред!";
            MessageBox.Show(instructions, "Инструкции", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}