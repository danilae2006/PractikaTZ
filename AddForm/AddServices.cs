using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.ShowForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.AddForm
{
    public partial class AddServices : Form
    {
        public AddServices()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        private void button2_Click(object sender, EventArgs e)
        {
            Sevices sevices = new Sevices();
            this.Visible = false;
            sevices.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                MySqlConnection con = new MySqlConnection(connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand($@"Insert Into services
(title,description,instructor,price)
Values ('{textBox1.Text}','{textBox4.Text}','{textBox2.Text}','{textBox3.Text}')", con);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Услуга добавлен");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void AddServices_Load(object sender, EventArgs e)
        {
            textBox4.Multiline = true;
            textBox4.ScrollBars = ScrollBars.Vertical;
            textBox4.Height = 150;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsLetter(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsLetter(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsLetter(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }
    }
}
