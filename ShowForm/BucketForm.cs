using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.AddForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Word = Microsoft.Office.Interop.Word;

namespace TZEgorov
{
    public partial class BucketForm : Form
    {
        public BucketForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        Dictionary<string, int> bucket;
        DateTime today;
        DateTime delivery;
        string conStr = data.conStr;
        string connect = data.conStr;
        int clientII = 0;
        public int priceOrder = 0;
        private readonly string FileName = Directory.GetCurrentDirectory() + @"\template\template.docx";

        private void BucketForm_Load(object sender, EventArgs e)
        {
            DateTime dateTimeNow = DateTime.Now;
            textBox2.Text = $"{data.usrSurname}" + $" {data.usrName}";
            maskedTextBox2.Text = dateTimeNow.ToString("yyyy-MM-dd");
            

            this.bucket = Bucket.MyBucket;
            string where = " title IN ('" + string.Join("', '", bucket.Keys.ToArray()) + "') ";

            FillDataGridView(where);
            GetClient();
        }

        private void GetClient()
        {
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select Surname, Name from `Client`";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox2.Items.Add(Rooms.Rows[i]["Surname"] + " " + Rooms.Rows[i]["Name"]);
            }
        }
        private void GetClientSearch()
        {
            string poisk = comboBox2.Text;
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = $@"select Surname, Name from `Client` Where Surname like '%{poisk}%'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }
            comboBox2.Items.Clear();
            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox2.Items.Add(Rooms.Rows[i]["Surname"] + " " + Rooms.Rows[i]["Name"]);
            }

            comboBox2.SelectionStart = comboBox2.Text.Length;
            comboBox2.SelectionLength = 0;
        }
        void FillDataGridView(string where = "")
        {
            // !!!!!
            string cmdStr = @"SELECT title AS 'Название', 
                                    Type AS 'Тип', 
                                    Description AS 'Описание',
                                    Name AS 'Производитель',
                                    price AS 'Цена' FROM products
                            INNER JOIN manufacture WHERE manufacture.idManufacture = products.Manufacter AND" + where;
            MySqlConnection con = new MySqlConnection(conStr);
            con.Open();
            MySqlCommand cmd = new MySqlCommand(cmdStr, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                priceOrder += Convert.ToInt32(dt.Rows[i]["Цена"]);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Add("count", "Количество");
            dataGridView1.AllowUserToAddRows = false; //!!!убрали строку для добавления

            //перебор строк для заполения столбца с количеством выбранного товара
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                string ProductArticleNumber = r.Cells["Название"].Value.ToString();
                r.Cells["count"].Value = bucket[ProductArticleNumber];
            }

            con.Close();
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            GetClientSearch();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            if (clientII == 1)
            {
                comboBox2.Items.Clear();
                GetClient();
            }
            clientII = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Ошибка", "Выбери клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string cmdPickupPoint = $"";
                MySqlConnection con = new MySqlConnection(conStr);
                con.Open();
                string Surname = comboBox2.Text;
                string[] words = Surname.Split(' ');

                MySqlCommand cmd = new MySqlCommand(cmdPickupPoint, con);
                MySqlCommand cmd2 = new MySqlCommand($"select idClient from `Client` where Surname = '{words[0]}';", con);
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
               



                int clientId = Convert.ToInt32(dt2.Rows[0].ItemArray.GetValue(0).ToString());
                string cmdOrder = string.Format(@"INSERT INTO `order`(Client, Seller, Order_date, Price, Status) 
                                                VALUES({0}, {1}, '{2}', {3}, 'Новый');", clientId,
                                                                                                data.userId,
                                                                                                maskedTextBox2.Text,
                                                                                                priceOrder
                                                                                                );
                //узнать ID последней добавленной записи
                string cmdLastId = "SELECT last_insert_id();";
                cmd.CommandText = cmdOrder + cmdLastId;
                string OrderID = cmd.ExecuteScalar().ToString();

                //orderproduct
                string cmdOrderProduct = @"INSERT INTO `listofproducts` (Product, Colvo, idOrder) VALUES ";
                string ProductArticleNumber, OrderProductCount;

                foreach (var item in bucket)
                {
                    ProductArticleNumber = item.Key.ToString();
                    OrderProductCount = item.Value.ToString();
                    cmdOrderProduct += string.Format("('{0}',{1},{2}),", ProductArticleNumber, OrderProductCount, OrderID);
                }

                cmd.CommandText = cmdOrderProduct.Substring(0, cmdOrderProduct.Length - 1);
                int res = cmd.ExecuteNonQuery();

                MessageBox.Show("Заказ сформирован", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                con.Close();

                Bucket.MyBucket.Clear();
                Check();
                Order order = new Order();
                this.Visible = false;
                order.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            this.Visible = false;
            order.ShowDialog();
            this.Close();
        }
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
        private void Check()
        {
            try
            {

                var wordApp = new Word.Application();
                wordApp.Visible = false;


                MySqlConnection con = new MySqlConnection(conStr);
                con.Open();
                string Surname = comboBox2.Text;
                string[] words = Surname.Split(' ');

                MySqlCommand cmd2 = new MySqlCommand($"select idOrder from `order` where Order_date = '{maskedTextBox2.Text}' AND Price = {priceOrder};", con);
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                string checkId = dt2.Rows[0].ItemArray.GetValue(0).ToString();

                try
                {
                    var wordDocument = wordApp.Documents.Open(FileName);
                    
                    ReplaceWordStub("{checkId}", checkId, wordDocument);
                    ReplaceWordStub("{Seller}", textBox2.Text, wordDocument);
                    ReplaceWordStub("{client}", comboBox2.Text, wordDocument);
                    ReplaceWordStub("{DateTime}", maskedTextBox2.Text, wordDocument);
                    ReplaceWordStub("{SUM}", Convert.ToString(priceOrder), wordDocument);

                    wordApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            

        }
    }
}
