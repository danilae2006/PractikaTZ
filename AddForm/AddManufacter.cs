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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.AddForm
{
    public partial class AddManufacter : Form
    {
        public AddManufacter()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        private void AddManufacter_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manufacter manufacter = new Manufacter();
            this.Visible = false;
            manufacter.ShowDialog();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlConnection con = new MySqlConnection(connect);
                con.Open();
                MySqlCommand cmd = new MySqlCommand($@"Insert Into `manufacture`
(Name)
Values ('{textBox1.Text}')", con);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Производитель добавлен добавлен");
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
