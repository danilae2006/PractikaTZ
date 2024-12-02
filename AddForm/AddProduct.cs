using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        public string fullPath;
        public string photoName;
        public int des1;
        private void button2_Click(object sender, EventArgs e)
        {
            Product viewAbb = new Product();
            this.Visible = false;
            viewAbb.ShowDialog();
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    if ((fileInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase)) &&
                        fileInfo.Length <= 2 * 1024 * 1024)
                    {
                        pictureBox3.Image = Image.FromFile(openFileDialog.FileName);
                        photoName = fileInfo.Name;
                        fullPath = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Выберите файл JPG или PNG размером не более 2 Мб.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox3.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox5.Text != "" && comboBox1.Text != "")
            {
                string name = textBox1.Text;
                string manufacture = comboBox1.Text;
                string description = textBox3.Text;
                int power = Convert.ToInt32(textBox2.Text);
                int price = Convert.ToInt32(textBox5.Text);
                int Stock_balance = Convert.ToInt32(textBox4.Text);
                string photo = photoName;
                string Type = comboBox2.Text;
                if (photo != String.Empty)
                {
                    string dest = @"./Photo/" + photo;
                    try
                    {
                        File.Copy(fullPath, dest);
                    }
                    catch
                    {
                        MessageBox.Show("Изображение уже загружено в директорию");
                    }
                }
                DataTable Rooms = new DataTable();
                using (MySqlConnection coon = new MySqlConnection(connect))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = coon;
                    cmd.CommandText = $@"select idManufacture from `manufacture` where name = '{manufacture}'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(Rooms);
                }

                for (int i = 0; i < Rooms.Rows.Count; i++)
                {
                    des1 = Convert.ToInt32(Rooms.Rows[i]["idManufacture"]);
                }

                string sqlQuery = $@"Insert Into products
(title,Description,Type,Manufacter,Stock_balance,price,power,photo)
Values ('{textBox1.Text}','{textBox3.Text}','{Type}','{des1}','{Stock_balance}','{price}','{power}','{photo}')";
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = connect;
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    int res = cmd.ExecuteNonQuery();

                    if (res == 1)
                    {
                        MessageBox.Show("Товар добавлен");

                    }
                    else
                    {
                        MessageBox.Show("Товар не добавлен");
                    }
                    textBox2.Text = null;
                    comboBox1.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox1.Text = null;
                    comboBox2.Text = null;
                    pictureBox3.Image = null;
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void AddProduct_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select name from `manufacture`";
    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox1.Items.Add(Rooms.Rows[i]["name"]);
            }
            textBox3.Multiline = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Height = 150;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AddManufacter addManufacter = new AddManufacter();
            addManufacter.ShowDialog();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            Manufacter();
        }
        private void Manufacter()
        {
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select name from `manufacture`";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox1.Items.Add(Rooms.Rows[i]["name"]);
            }
        }
    }
}
