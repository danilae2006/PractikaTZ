using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZEgorov.AddForm
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(textBox1.Text);

            Properties.Settings.Default.Time = Convert.ToString(time*1000);
            Properties.Settings.Default.Save();
            MessageBox.Show("Настройки изменены", "Настройка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.ShowDialog();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            int timebox = Convert.ToInt32(Properties.Settings.Default.Time) / 1000;
            textBox1.Text = Convert.ToString(timebox);
        }
    }
}
