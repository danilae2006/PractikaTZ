using MySql.Data.MySqlClient;
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
using TZEgorov.ShowForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Word = Microsoft.Office.Interop.Word;

namespace TZEgorov
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        public string TypeStr = "";
        private readonly string FileName = Directory.GetCurrentDirectory() + @"\template\template1.docx";
        bool form = false;
        string connect = data.conStr;
        DataView dv;
        public int priceBox2 = 0;
        public string stock;
        int currentRowIndex;
        Dictionary<string, int> bucket = new Dictionary<string, int>();
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }

        private void Order_Load(object sender, EventArgs e)
        {
            button4.Visible = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            GetDate();
            button5.Enabled = false;
            if (data.role == "Продавец")
            {
                button5.Visible = false;
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }
        private void GetDate()
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;

                con.Open();

                MySqlCommand cmd = new MySqlCommand("select idOrder, o.Surname AS 'Клиент', n.Surname AS 'Продавец', Order_date AS 'Дата заказа',Price AS 'Цена, руб.' from `order` INNER JOIN `client` AS `o` ON Client = o.idClient INNER JOIN  `user` AS `n` ON Seller = n.UserID;", con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            dataGridView1.Columns["idOrder"].Visible = false;

        }
        private void GetDate2()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(data.conStr))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"select title AS 'Название', Type AS 'Тип', Description AS 'Описание', o.Name AS 'Производитель'," +
                        $" Power AS 'Мощность в Дж.',Price AS 'Цена',Stock_balance AS 'Количество' from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture WHERE Stock_balance > 0;", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Width = 965;
            GetDate2();
            form = true;
            button1.Enabled = false;
            button5.Enabled = false;
        }

        void contextmenu_click(object sender, EventArgs e)
        {
            string ProductArticleNumber = dataGridView1.Rows[currentRowIndex].Cells["Название"].Value.ToString();

            dataGridView1.Rows[currentRowIndex].Selected = false;
            //cart.Add(ProductArticleNumber);

            try
            {
                bucket.Add(ProductArticleNumber, 1);
            }
            catch (ArgumentException)
            {
                bucket[ProductArticleNumber] = bucket[ProductArticleNumber] + 1;
            }

            button4.Visible = true;
            //button1.Text = cart.Count.ToString() + " товар(ов)";
            button4.Text = bucket.Count + " товара(ов)";
        }
        void delete(object sender, EventArgs e)
        {
            int idOrder = Convert.ToInt32(dataGridView1.Rows[currentRowIndex].Cells["idOrder"].Value.ToString());
            string strCon = data.conStr;
            string strCmd = $"DELETE FROM `listofproducts` WHERE idOrder = {idOrder}";
            string strCmd2 = $"DELETE FROM `order` WHERE idOrder = {idOrder}";
            using (MySqlConnection con = new MySqlConnection())
            {
                try
                {

                    con.ConnectionString = strCon;

                    con.Open();


                    MySqlCommand cmd = new MySqlCommand(strCmd, con);
                    MySqlCommand cmd2 = new MySqlCommand(strCmd2, con);

                    DialogResult dr = MessageBox.Show("Удалить запись ?", "Внимание!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Yes)
                    {
                        int res = cmd.ExecuteNonQuery();
                        int res2 = cmd2.ExecuteNonQuery();
                        MessageBox.Show("Запись удалена", "Внимание!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        GetDate();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        void Sostav(object sender, EventArgs e)
        {
            Bucket.IDorder = dataGridView1.Rows[currentRowIndex].Cells["idOrder"].Value.ToString();
            ListOfProduct listOfProduct = new ListOfProduct();
            listOfProduct.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bucket.MyBucket = bucket;

            BucketForm bucketForm = new BucketForm();
            this.Visible = false;
            bucketForm.ShowDialog();
            this.Close();

            bucket = Bucket.MyBucket;
            button4.Visible = false;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

            if (form == false)
            {
                try
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        ContextMenu m = new ContextMenu();
                        m.MenuItems.Add(new MenuItem("Состав заказа", Sostav));
                        if (data.role == "Продавец")
                        {

                        }
                        else
                        {
                            m.MenuItems.Add(new MenuItem("Удалить запись", delete));
                        }
                        this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                        dataGridView1.Rows[currentRowIndex].Selected = true;

                        m.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
            }
            else
            {
                try
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        ContextMenu m = new ContextMenu();
                        m.MenuItems.Add(new MenuItem("В корзину", contextmenu_click));

                        this.currentRowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                        dataGridView1.Rows[currentRowIndex].Selected = true;

                        m.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
                catch (Exception ex)
                {
                return;
            }

        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetDate();
            this.Width = 812;
            form = false;
            button1.Enabled = true;
            button5.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                MySqlConnection connection = new MySqlConnection(connect);
                DataTable dt1 = new DataTable();

                connection.Open();

                MySqlCommand sql1 = new MySqlCommand($"select idOrder, o.Surname AS 'Клиент', n.Surname AS 'Продавец', Order_date AS 'Дата заказа',Price AS 'Цена, руб.',Status AS 'Статус' from `order` INNER JOIN `client` AS `o` ON Client = o.idClient INNER JOIN  `user` AS `n` ON Seller = n.UserID where idOrder= {Convert.ToInt32(id)};");
                sql1.Connection = connection;
                sql1.ExecuteNonQuery();

                MySqlDataAdapter da1 = new MySqlDataAdapter(sql1);
                da1.Fill(dt1);
                string Applicant = dt1.Rows[0].ItemArray.GetValue(1).ToString();
                string Specialization = dt1.Rows[0].ItemArray.GetValue(2).ToString();
                string date = dt1.Rows[0].ItemArray.GetValue(3).ToString();
                string Score = dt1.Rows[0].ItemArray.GetValue(4).ToString();

                var wordApp = new Word.Application();
                wordApp.Visible = false;

                try
                {
                    var wordDocument = wordApp.Documents.Open(FileName);

                    ReplaceWordStub("{Applicant}", Applicant, wordDocument);
                    ReplaceWordStub("{Specialization}", Specialization, wordDocument);
                    ReplaceWordStub("{date}", date, wordDocument);
                    ReplaceWordStub("{Score}", Score, wordDocument);

                    wordApp.Visible = true;
                    button5.Enabled = false;
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
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        int id_order = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                try
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        id_order = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);
                        button5.Enabled = true;
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Пистолет")
            {
                TypeStr = "Пистолет";
                Filtration();
            }
            else if (comboBox3.Text == "Очистить")
            {
                GetDate2();
                comboBox3.Text = "";
            }
            else if (comboBox3.Text == "Револьвер")
            {
                TypeStr = "Револьвер";
                Filtration();
            }
            else if (comboBox3.Text == "Винтовка")
            {
                TypeStr = "Винтовка";
                Filtration();
            }
            else if (comboBox3.Text == "Патроны")
            {
                TypeStr = "Патроны";
                Filtration();
            }
            else if (comboBox3.Text == "Балон")
            {
                TypeStr = "Балон";
                Filtration();
            }
            else if (comboBox3.Text == "Тюнинг")
            {
                TypeStr = "Тюнинг";
                Filtration();
            }
            else if (comboBox3.Text == "Аксессуар")
            {
                TypeStr = "Аксессуар";
                Filtration();
            }
            
        }
        private void Filtration()
        {
            try
            {

                using (MySqlConnection con = new MySqlConnection(data.conStr))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand($"select title AS 'Название', Type AS 'Тип', Description AS 'Описание', o.Name AS 'Производитель'," +
                        $" Power AS 'Мощность в Дж.',Price AS 'Цена',Stock_balance AS 'Количество' from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture WHERE Stock_balance > 0 AND Type = '{TypeStr}';", con);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
