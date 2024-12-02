using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.ShowForm
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        int m1 = 0;
        int m2 = 0;
        DataGridView dgv;
        string tableName;
        private void Users_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select UserID, Name AS 'Имя', Surname AS 'Фамилия',Patronyc AS 'Отчество', Login AS 'Логин', Password AS 'Пароль', Role AS 'Роль', Phone AS 'Телефон', Pasport AS 'Паспорт' from user;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUpdateForm.DataSource = dt;
                dgvUpdateForm.Columns["UserID"].Visible = false;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
                Admin admin = new Admin();
                this.Visible = false;
                admin.ShowDialog();
                this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUsers addUsers = new AddUsers();
            this.Visible = false;
            addUsers.ShowDialog();
            this.Close();
        }

        private void dgvUpdateForm_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        private void dgvUpdateForm_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.Value != null && m1 == 0)
            {
                dgvUpdateForm.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('\u25CF', e.Value.ToString().Length);

            }
            else if (e.ColumnIndex == 7 && e.Value != null && m2 == 0)
            {
                dgvUpdateForm.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('\u25CF', e.Value.ToString().Length);

            }
            else if (m1 == 1 && m2 == 0)
            {

            }
        }

        private void dgvUpdateForm_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex1 = e.RowIndex;
            if (rowIndex1 >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        tableName = "user";
                        dgv = (DataGridView)sender;
                        int rowIndex = e.RowIndex;
                        dgv.Rows[rowIndex].Selected = true;
                        string cell0 = dgv.Rows[rowIndex].Cells[0].Value.ToString();
                        string strCmd = "DELETE FROM " + tableName + " WHERE ";
                        string strCon = data.conStr;
                        switch (tableName)
                        {
                            case "user":
                                strCmd += "UserID='" + cell0 + "';";
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
                                    if (Convert.ToInt32(cell0) == data.userId)
                                    {
                                        MessageBox.Show("Невозможно удалить пользователя, под которым совершён вход.");
                                    }
                                    else
                                    {
                                        int res = cmd.ExecuteNonQuery();
                                        MessageBox.Show("Удалено " + res.ToString(), "Внимание!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                                        GetDate();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw;
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
                    if (e.ColumnIndex == 8 && m1 == 0)
                    {
                        m1 = 1;

                    }
                    else if (e.ColumnIndex == 7 && m2 == 0)
                    {
                        m2 = 1;

                    }
                    else if (e.ColumnIndex == 8 && m1 == 1)
                    {
                        m1 = 0;
                    }
                    else if (e.ColumnIndex == 7 && m2 == 1)
                    {
                        m2 = 0;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Width = 1125;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Width = 822;
        }

        private void dgvUpdateForm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                string Names = dgvUpdateForm.Rows[rowIndex].Cells["UserID"].Value.ToString();
                string sqlQuery = "select Name, Surname,Patronyc, Login, Password, Role, Phone, Pasport from user WHERE UserID='" + Names + "';";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                textBox1.Text = rdr["Name"].ToString();
                textBox2.Text = rdr["Surname"].ToString();
                textBox3.Text = rdr["Patronyc"].ToString();
                textBox4.Text = rdr["Login"].ToString();
                comboBox1.Text = rdr["Role"].ToString();
                maskedTextBox1.Text = rdr["Phone"].ToString();
                maskedTextBox2.Text = rdr["Pasport"].ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                int rowIndex = dgvUpdateForm.CurrentCell.RowIndex;
                string Names = dgvUpdateForm.Rows[rowIndex].Cells["UserID"].Value.ToString();

                string name = textBox1.Text;
                string surname = textBox2.Text;
                string patronyk = textBox3.Text;
                string login = textBox4.Text;
                string password = GetHashPass(textBox5.Text.ToString()); ;
                string role = comboBox1.Text;
                string phone = maskedTextBox1.Text;
                string passport = maskedTextBox2.Text;



                string sqlQuery = $@"UPDATE user SET name = '{name}', surname = '{surname}' ,Patronyc = '{patronyk}',login = '{login}', " +
                            $"password = '{password}', Role = '{role}', phone = '{phone}', pasport = '{passport}' WHERE UserID = '{Names}'";
                using (MySqlConnection con = new MySqlConnection())
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
                    textBox1.Text = null;
                    textBox2.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    comboBox1.Text = null;
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
