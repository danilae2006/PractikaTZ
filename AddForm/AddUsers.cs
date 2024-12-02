using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.ShowForm;

namespace TZEgorov
{
    public partial class AddUsers : Form
    {
        public AddUsers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        private void button1_Click(object sender, EventArgs e)
        {
            string password = GetHashPass(textBox5.Text.ToString());
            if (textBox1.Text != "" || comboBox1.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox2.Text != "" || maskedTextBox1.Text != "" || maskedTextBox2.Text != "")
            {
                MySqlConnection con = new MySqlConnection(connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand($@"Insert Into user
(Name,Surname,Patronyc,Login,Password,Role,Phone,Pasport)
Values ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{password}','{comboBox1.Text}','{maskedTextBox1.Text}','{maskedTextBox2.Text}')", con);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Пользователь добавлен");
                textBox1.Text = "";
                comboBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        public static string GetHashPass(string password)
        {

            byte[] bytesPass = Encoding.UTF8.GetBytes(password);

            SHA256Managed hashstring = new SHA256Managed();

            byte[] hash = hashstring.ComputeHash(bytesPass);

            string hashPasswd = string.Empty;

            foreach (byte x in hash)
            {

                hashPasswd += String.Format("{0:x2}", x);
            }

            hashstring.Dispose();

            return hashPasswd;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = CreatePassword(15);
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyz_-ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            this.Visible = false;
            users.ShowDialog();
            this.Close();
        }
        private bool CharCorrectLogin(char c)
        {

            return (c >= 'а' && c <= 'я') ||
                   (c >= 'А' && c <= 'Я');


        }
        private bool CharCorrectLogin2(char c)
        {

            return (c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= '0' && c <= '9') ||
                    (c == '-' && c == '_') ||
                    (c == '!' && c == '@');

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

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin2(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin2(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                int cursorPosition = textBox3.SelectionStart;
                textBox3.Text = char.ToUpper(textBox3.Text[0]) + textBox3.Text.Substring(1);
                textBox3.SelectionStart = cursorPosition;
            }
        }
    }
}
