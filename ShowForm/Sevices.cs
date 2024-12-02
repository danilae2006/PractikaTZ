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
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace TZEgorov.ShowForm
{
    public partial class Sevices : Form
    {
        public Sevices()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        string poisk = "";
        string sort = "ASC";
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void Sevices_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            textBox3.Multiline = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Height = 150;
            if (data.role == "Продавец")
            {
                button1.Visible = false;
                button2.Visible = false;
            }
        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select title AS 'Название', description AS 'Описание', instructor AS 'Инструктор', Price AS 'Цена' from services;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddServices addServices = new AddServices();
            this.Visible = false;
            addServices.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Width = 1053;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void SortBox()
        {

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select title AS 'Название', description AS 'Описание', instructor AS 'Инструктор', Price AS 'Цена' from services WHERE title like '%{poisk}%';", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }
        private void SearchBox()
        {

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select title AS 'Название', description AS 'Описание', instructor AS 'Инструктор', Price AS 'Цена' from services ORDER BY Price {sort};", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            poisk = textBox1.Text;
            SortBox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            GetDate();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            sort = "ASC";
            SearchBox();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            sort = "DESC";
            SearchBox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Width = 699;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                string Names = dataGridView1.Rows[rowIndex].Cells["Название"].Value.ToString();
                string sqlQuery = "select title AS 'Название', description AS 'Описание', instructor AS 'Инструктор', Price AS 'Цена' from services WHERE title='" + Names + "';";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                textBox2.Text = rdr["Название"].ToString();
                textBox3.Text = rdr["Описание"].ToString();
                textBox4.Text = rdr["Инструктор"].ToString();
                textBox5.Text = rdr["Цена"].ToString();

                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Выберите услугу для редактирования.");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string Names = dataGridView1.Rows[rowIndex].Cells["Название"].Value.ToString();

                string name = textBox2.Text;
                string description = textBox3.Text;
                string instructor = textBox4.Text;
                int price = Convert.ToInt32(textBox5.Text);



                string sqlQuery = $@"UPDATE services SET title = '{name}',Description = '{description}',instructor = '{instructor}',  " +
                            $"price = {price} WHERE title = '{Names}'";
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = connect;
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    int res = cmd.ExecuteNonQuery();

                    if (res == 1)
                    {
                        GetDate();
                        MessageBox.Show("данные обновлены");

                    }
                    else
                    {
                        MessageBox.Show("данные не обновлены");
                    }
                    textBox2.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                }
            }
        }
    }
}
