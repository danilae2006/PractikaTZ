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
using System.Xml.Linq;
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.ShowForm
{
    public partial class Services_performed : Form
    {
        public Services_performed()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        int currentRowIndex;
        
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void Services_performed_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select id, o.title AS 'Услуга', n.Surname AS 'Клиент', Date AS 'Дата проведения', Status AS 'Статус' from `services_performed` INNER JOIN `services` AS `o` ON idServices_performed = o.idServices INNER JOIN  `client` AS `n` ON Client = n.idClient;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["id"].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPerformed addPerformed = new AddPerformed();
            this.Visible = false;
            addPerformed.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var CellValue = dataGridView1.Rows[e.RowIndex].Cells["Статус"].Value;
                if (CellValue != null)
                {
                    string statusVIP = CellValue.ToString();
                    switch (statusVIP)
                    {
                        case "Завершённый":
                            e.CellStyle.BackColor = Color.Aqua;
                            break;
                        case "Новый":
                            e.CellStyle.BackColor = Color.White;
                            break;
                        case "Отменён":
                            e.CellStyle.BackColor = Color.HotPink;
                            break;
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                int Names = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["id"].Value.ToString());
                comboBox3.Enabled = true;
                button2.Enabled = true;
                string sqlQuery = "select id, Status from `services_performed` WHERE id=" + Names + ";";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                comboBox3.Text = rdr["Status"].ToString();
                
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            string Names = dataGridView1.Rows[rowIndex].Cells["id"].Value.ToString();
            string status = comboBox3.Text;

            string sqlQuery = $@"UPDATE services_performed SET Status = '{status}' WHERE id = '{Names}'";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    MessageBox.Show("Статус изменён");

                }
                else
                {
                    MessageBox.Show("Статус не изменён");
                }
                comboBox3.Text = null;
                comboBox3.Enabled = false;
                button2.Enabled = false;
            }

            GetDate();
        }
    }
}
