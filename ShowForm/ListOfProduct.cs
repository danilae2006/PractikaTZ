using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.ShowForm
{
    public partial class ListOfProduct : Form
    {
        public ListOfProduct()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListOfProduct_Load(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(Bucket.IDorder);
            try
            {
                try
                {

                    using (MySqlConnection con = new MySqlConnection(data.conStr))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand($"select idOrder AS 'Номер заказа', Product AS 'Товар'," +
                            $" Colvo AS 'Количество.' from listofproducts WHERE idOrder = {orderId};", con);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns["Номер заказа"].Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (ArgumentException)
            {
                return;
            }
        }
    }
}
