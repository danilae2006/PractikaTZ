﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        DataGridView dgv;
        string tableName;
        public int minScren = 0;
        public int maxScren = 10;
        public int count = 0;
        public int countlast;

        public string search = "";
        private void button2_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
            tableName = "client";
            label4.ForeColor = Color.Aqua;
            dgvUpdateForm.Rows[0].Selected = false;
        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select * from `client`;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
                this.dgvUpdateForm.Columns["idClient"].Visible = false;
                countlast = dgvUpdateForm.RowCount;
                label3.Text = Convert.ToString("Общее кол-во строк: " + count);
            }
            Search();
        }
        private void Search()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select idClient, Name AS 'Имя', Surname AS 'Фамилия', Phone AS 'Телефон', Pasport AS 'Паспорт' from `client` WHERE Name LIKE '%{search}%' OR Surname LIKE '%{search}%' LIMIT {minScren},{maxScren};", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
                this.dgvUpdateForm.Columns["idClient"].Visible = false;
                count = dgvUpdateForm.RowCount;
                label3.Text = Convert.ToString("Общее кол-во строк: " + count);
                dgvUpdateForm.RowHeadersVisible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddClient addClient = new AddClient();
            this.Visible = false;
            addClient.ShowDialog();
            this.Close();
        }



        private void dgvUpdateForm_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (data.role == "Продавец")
            {

            }
            else
            {
                int rowIndex1 = e.RowIndex;
                if (rowIndex1 >= 0)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        try
                        {

                            dgv = (DataGridView)sender;
                            int rowIndex = e.RowIndex;
                            dgv.Rows[rowIndex].Selected = true;
                            string cell0 = dgv.Rows[rowIndex].Cells[0].Value.ToString();
                            string strCmd = "DELETE FROM " + tableName + " WHERE ";
                            string strCon = data.conStr;
                            switch (tableName)
                            {
                                case "client":
                                    strCmd += "idClient='" + cell0 + "';";
                                    break;
                            }
                            using (MySqlConnection con = new MySqlConnection())
                            {
                                try
                                {

                                    con.ConnectionString = strCon;

                                    con.Open();


                                    MySqlCommand cmd = new MySqlCommand(strCmd, con);

                                    DialogResult dr = MessageBox.Show("Удалить запись ?", "Внимание!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (dr == DialogResult.Yes)
                                    {
                                        int res = cmd.ExecuteNonQuery();
                                        MessageBox.Show("Удалено " + res.ToString(), "Внимание!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                        GetDate();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Запись связана с другой таблицей. Невозможно удалить");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                    else if (e.Button == MouseButtons.Left)
                    {

                    }
                }
            }
        }

        private void dgvUpdateForm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                string Names = dgvUpdateForm.Rows[rowIndex].Cells["idClient"].Value.ToString();
                string sqlQuery = "select Name, Surname, Phone, Pasport from `client` WHERE idClient='" + Names + "';";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                textBox1.Text = rdr["Name"].ToString();
                textBox2.Text = rdr["Surname"].ToString();
                maskedTextBox1.Text = rdr["Phone"].ToString();
                maskedTextBox2.Text = rdr["Pasport"].ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Width = 1128;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Width = 824;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                int rowIndex = dgvUpdateForm.CurrentCell.RowIndex;
                string Names = dgvUpdateForm.Rows[rowIndex].Cells["idClient"].Value.ToString();

                string name = textBox1.Text;
                string surname = textBox2.Text;
                string phone = maskedTextBox1.Text;
                string passport = maskedTextBox2.Text;



                string sqlQuery = $@"UPDATE client SET name = '{name}', surname = '{surname}' , phone = '{phone}', pasport = '{passport}' WHERE idClient = {Names}";
                using (MySqlConnection con = new MySqlConnection())
                {
                    try
                    {
                        con.ConnectionString = connect;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                        int res = cmd.ExecuteNonQuery();


                        if (res == 1)
                        {
                            MessageBox.Show("данные обновлены");

                        }
                        else
                        {
                            MessageBox.Show("данные не обновлены");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    textBox1.Text = null;
                    textBox2.Text = null;
                    maskedTextBox1.Text = null;
                    maskedTextBox2.Text = null;
                }
                GetDate();

            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (maxScren <= count)
            {
                minScren += 10;
                if (minScren == 0)
                {
                    label4.ForeColor = Color.Aqua;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 10)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Aqua;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 20)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Aqua;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 30)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Aqua;
                }
                Search();
            }
            else
            {
                minScren = 0;
                maxScren = 10;
                label4.ForeColor = Color.Aqua;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                Search();

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (minScren != 0)
            {
                minScren -= 10;
                if (minScren == 0)
                {
                    label4.ForeColor = Color.Aqua;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 10)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Aqua;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 20)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Aqua;
                    label9.ForeColor = Color.Black;
                }
                else if (minScren == 30)
                {
                    label4.ForeColor = Color.Black;
                    label5.ForeColor = Color.Black;
                    label6.ForeColor = Color.Black;
                    label9.ForeColor = Color.Aqua;
                }
                Search();
            }
            else
            {
                minScren = countlast - 1;
                maxScren = 10;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label9.ForeColor = Color.Aqua;
                Search();
            }

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            search = textBox3.Text;
            minScren = 0;
            maxScren = 10;
            Search();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            minScren = 0;
            Search();
            label4.ForeColor = Color.Aqua;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            minScren = 10;
            Search();
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Aqua;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;


        }

        private void label6_Click(object sender, EventArgs e)
        {
            minScren = 20;
            Search();
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Aqua;
            label9.ForeColor = Color.Black;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            minScren = 30;
            Search();
            label4.ForeColor = Color.Black;
            label5.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
            label9.ForeColor = Color.Aqua;
        }

        private void label9_MouseHover(object sender, EventArgs e)
        {
            if (label9.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label9.ForeColor = Color.Red;
            }
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            if (label6.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label6.ForeColor = Color.Red;
            }
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            if (label5.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label5.ForeColor = Color.Red;
            }
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            if (label4.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label4.ForeColor = Color.Red;
            }
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            if (label9.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label9.ForeColor = Color.Black;
            }
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            if (label6.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label6.ForeColor = Color.Black;
            }
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            if (label5.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label5.ForeColor = Color.Black;
            }
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            if (label4.ForeColor == Color.Aqua)
            {

            }
            else
            {
                label4.ForeColor = Color.Black;
            }
        }
    }
}
