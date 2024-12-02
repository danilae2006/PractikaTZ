using MySql.Data.MySqlClient;
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

namespace TZEgorov
{
    public partial class Manufacter : Form
    {
        public Manufacter()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        DataGridView dgv;
        string tableName;
        public int deletRow = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddManufacter addManufacter = new AddManufacter();
            this.Visible = false;
            addManufacter.ShowDialog();
            this.Close();
        }

        private void Manufacter_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
            tableName = "manufacture";
            if (data.role == "Продавец")
            {
                button1.Visible = false;
            }
        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select Name AS 'Производитель' from `manufacture`;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                            case "manufacture":
                                strCmd += "Name='" + cell0 + "';";
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
    }
}
