using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace TZEgorov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private string captchaText;
        string conString = data.conStr;
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }
                else
                {
                    string login = textBox1.Text.ToString();
                    string userName = string.Empty;
                    string userSurname = string.Empty;
                    string userPatr = string.Empty;
                    string userId = string.Empty;
                    string hashPassword = string.Empty;
                    string role = string.Empty;
                    MySqlConnection con = new MySqlConnection(conString);
                    con.Open();

                    MySqlCommand cmd = new MySqlCommand($"Select * From User Where Login = '{login}'", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    hashPassword = GetHashPass(passwd.Text.ToString());
                    userName = dt.Rows[0].ItemArray.GetValue(1).ToString();
                    userSurname = dt.Rows[0].ItemArray.GetValue(2).ToString();
                    userPatr = dt.Rows[0].ItemArray.GetValue(3).ToString();
                    userId = dt.Rows[0].ItemArray.GetValue(0).ToString();
                    data.usrName = userName;
                    data.usrSurname = userSurname;
                    data.usrPatr = userPatr;
                    data.userId = Convert.ToInt32(userId);

                    if (hashPassword == dt.Rows[0].ItemArray.GetValue(5).ToString())
                    {
                        role = dt.Rows[0].ItemArray.GetValue(6).ToString();
                        data.role = role;
                        MessageBox.Show("Вы успешно авторизовались");
                        Admin admin = new Admin();
                        this.Visible = false;
                        admin.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                        passwd.Text = "";
                        Captha();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Captha();
            }
        }
        private void Captha()
        {
            CaptchaToImage();
            button2.Enabled = false;
            textBox1.Enabled = false;
            passwd.Enabled = false;
            textBox1.Text = null;
            passwd.Text = null;
            this.Width = 930;
        }
        private void CaptchaToImage()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            captchaText = ""; for (int i = 0; i < 5; i++)
            {
                captchaText += chars[random.Next(chars.Length)];
            }
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.SmoothingMode = SmoothingMode.AntiAlias; graphics.Clear(Color.White);
            Font font = new Font("Arial", 20, FontStyle.Bold);
            for (int i = 0; i < 5; i++)
            {
                PointF point = new PointF(i * 20, 0);
                graphics.TranslateTransform(10, 10);
                graphics.RotateTransform(random.Next(-10, 10));
                graphics.DrawString(captchaText[i].ToString(), font, Brushes.Black, point);
                graphics.ResetTransform();
            }
            for (int i = 0; i < 10; i++)
            {
                Pen pen = new Pen(Color.Black, random.Next(2, 5));
                int x1 = random.Next(pictureBox2.Width);
                int y1 = random.Next(pictureBox2.Height);
                int x2 = random.Next(pictureBox2.Width);
                int y2 = random.Next(pictureBox2.Height); graphics.DrawLine(pen, x1, y1, x2, y2);
            }
            pictureBox2.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Captha();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == captchaText)
            {
                MessageBox.Show("Успешный ввод");
                button2.Enabled = true;
                textBox1.Enabled = true;
                passwd.Enabled = true;
                textBox2.Text = null;
                this.Width = 577;
            }
            else
            {
                MessageBox.Show("Неверный ввод, блокировка системы на 10 секунд");
                button5.Enabled = false;
                button4.Enabled = false;
                Thread.Sleep(10000);
                button5.Enabled = true;
                button4.Enabled = true;
                Captha();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private bool CharCorrectLogin(char c)
        {

            return (c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= '0' && c <= '9') ||
                    (c == 32);

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            passwd.PasswordChar = default;
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
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            passwd.PasswordChar = '*';
        }

        private void passwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!CharCorrectLogin2(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                char ch = e.KeyChar;

                if (ch != '-' && ch != '_')
                {
                    e.Handled = true;
                }
            }

        }
        private bool CharCorrectLogin2(char c)
        {

            return (c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= '0' && c <= '9') ||
                    (c == '-' && c == '_') ||
                    (c == 32);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
    }
}
