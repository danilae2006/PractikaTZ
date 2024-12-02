using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.AddForm
{
    public partial class AddClient : Form
    {
        public AddClient()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        private void button2_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            this.Visible = false;
            client.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && maskedTextBox1.Text != "" && maskedTextBox2.Text != "")
            {
                MySqlConnection con = new MySqlConnection(connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand($@"Insert Into client
(Name,Surname,Phone,Pasport)
Values ('{textBox1.Text}','{textBox2.Text}','{maskedTextBox1.Text}','{maskedTextBox2.Text}')", con);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Клиент добавлен");
                textBox1.Text = "";
                textBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private bool CharCorrectLogin(char c)
        {

            return (c >= 'а' && c <= 'я') ||
                   (c >= 'А' && c <= 'Я');


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddClient_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                int cursorPosition = textBox1.SelectionStart;
                textBox1.Text = char.ToUpper(textBox1.Text[0]) + textBox1.Text.Substring(1);
                textBox1.SelectionStart = cursorPosition;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                int cursorPosition = textBox2.SelectionStart;
                textBox2.Text = char.ToUpper(textBox2.Text[0]) + textBox2.Text.Substring(1);
                textBox2.SelectionStart = cursorPosition;
            }
        }


    }
}
