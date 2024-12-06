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
    public partial class DataUser : Form
    {
        public DataUser(string UserID)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            GetDate(UserID);
        }
        string connect = data.conStr;
        private void DataUser_Load(object sender, EventArgs e)
        {
            
        }
        private void GetDate(string userId)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand($"select UserID, Name AS 'Имя', Surname AS 'Фамилия',Patronyc AS 'Отчество', Login AS 'Логин', Password AS 'Пароль', Role AS 'Роль', Phone AS 'Телефон', Pasport AS 'Паспорт' from user WHERE UserID = {Convert.ToInt32(userId)};", con);
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
            this.Close();
        }
    }
}
