using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace TZEgorov
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
        }
        DataGridView dgv;
        string connect = data.conStr;
        DataView dv;
        string poisk = "";
        string sort = "ASC";
        string price = "title";
        public int des1;
        public string imagepath;
        public string fullPath;
        public string photoName = "";
        public string photoCheck;
        public string TypeStr = "";
        public int power = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();

        }
        private void Product_Load(object sender, EventArgs e)
        {
            if (data.role == "Продавец")
            {
                button1.Visible = false;
                button2.Visible = false;
            }
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            textBox3.Multiline = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Height = 150;
            viewTable();
            fillComboBox();
            button4.Enabled = false;
        }
        private void fillComboBox()
        {
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select name from `manufacture`";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox4.Items.Add(Rooms.Rows[i]["name"]);
            }
        }
        private void viewTable()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(data.conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select title,Type, Description, o.Name AS 'Производитель'," +
                    $" Power,Price,Stock_balance,Photo from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture;", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 7)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            string photo = dt.Rows[i][j].ToString();
                            try
                            {
                                if (photo == "")
                                {
                                    photo = "picture.png";
                                }
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                        }
                            catch (Exception)
                            {
                            photo = "picture.png";
                            Image sketch = new Bitmap($@"{data.path}\{photo}");
                            dataGridView1.Rows[i].Cells[j].Value = sketch;
                        }
                    }
                    }
                }
            }
            photoName = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            this.Visible = false;
            addProduct.ShowDialog();
            this.Close();
        }
        private void SearchBox()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(data.conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select title,Type, Description, o.Name AS 'Производитель'," +
                    $" Power,Price,Stock_balance,Photo from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture ORDER BY Price {sort};", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 7)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            string photo = dt.Rows[i][j].ToString();
                            try
                            {
                                if (photo == "")
                                {
                                    photo = "picture.png";
                                }
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                            catch (Exception)
                            {
                                photo = "picture.png";
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                        }
                    }
                }
            }
            photoName = "";
        }
        private void SortBox()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(data.conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select title,Type, Description, o.Name AS 'Производитель'," +
                    $" Power,Price,Stock_balance,Photo from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture WHERE `title` like '%{poisk}%';", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 7)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            string photo = dt.Rows[i][j].ToString();
                            try
                            {
                                if (photo == "")
                                {
                                    photo = "picture.png";
                                }
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                            catch (Exception)
                            {
                                photo = "picture.png";
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                        }
                    }
                }
            }
            photoName = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == null || comboBox3.Text == "")
            {
                poisk = textBox1.Text;
                SortBox();
            }
            else
            {
                poisk = textBox1.Text;
                Filtration();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Width = 1388;
            if (textBox2.Text != "")
            {
                button4.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Выберите товар для редактирования.");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string Names = dataGridView1.Rows[rowIndex].Cells["Название"].Value.ToString();

                string name = textBox2.Text;
                string manufacture = comboBox4.Text;
                string description = textBox3.Text;
                if (textBox4.Text == "")
                {
                    power = 0;
                }
                else
                {
                    power = Convert.ToInt32(textBox4.Text);
                }
                int price = Convert.ToInt32(textBox5.Text);
                int Stock_balance = Convert.ToInt32(textBox6.Text);
                string Type = comboBox1.Text;
                //

                //
                string photo = photoName;

                if (photo == "")
                {
                    DataTable Rooms1 = new DataTable();
                    using (MySqlConnection coon = new MySqlConnection(connect))
                    {
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = coon;
                        cmd.CommandText = $@"select Photo from `products` where Title = '{name}'";
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(Rooms1);
                    }

                    for (int i = 0; i < Rooms1.Rows.Count; i++)
                    {
                        photo = Convert.ToString(Rooms1.Rows[i]["Photo"]);
                        photoCheck = Convert.ToString(Rooms1.Rows[i]["Photo"]);
                    }
                }
                if (photo != String.Empty && photo != photoCheck)
                {
                    string dest = @"./Photo/" + photo;
                    try
                    {
                        File.Copy(fullPath, dest);
                    }
                    catch
                    {
                        MessageBox.Show("Изображение уже загружено в директорию");
                    }
                }
                DataTable Rooms = new DataTable();
                using (MySqlConnection coon = new MySqlConnection(connect))
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = coon;
                    cmd.CommandText = $@"select idManufacture from `manufacture` where name = '{manufacture}'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(Rooms);
                }

                for (int i = 0; i < Rooms.Rows.Count; i++)
                {
                    des1 = Convert.ToInt32(Rooms.Rows[i]["idManufacture"]);
                }

                string sqlQuery = $@"UPDATE products SET title = '{name}', Type = '{Type}' ,Description = '{description}',Manufacter = '{des1}', " +
                            $"Stock_balance = '{Stock_balance}', price = '{price}', power = '{power}', photo = '{photo}' WHERE title = '{Names}'";
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = connect;
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                    int res = cmd.ExecuteNonQuery();

                    if (res == 1)
                    {
                        viewTable();
                        MessageBox.Show("данные обновлены");

                    }
                    else
                    {
                        MessageBox.Show("данные не обновлены");
                    }
                    textBox2.Text = null;
                    comboBox4.Text = null;
                    textBox3.Text = null;
                    textBox4.Text = null;
                    textBox5.Text = null;
                    textBox6.Text = null;
                    pictureBox3.Image = null;
                    comboBox1.Text = null;
                }

                viewTable();
            }
        }
        private void textBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Width = 1031;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == null || comboBox3.Text == "")
            {
                radioButton2.Checked = false;
                sort = "ASC";
                SearchBox();
            }
            else
            {
                radioButton2.Checked = false;
                sort = "ASC";
                Filtration();
            }

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == null || comboBox3.Text == "")
            {
                radioButton1.Checked = false;
                sort = "DESC";
                SearchBox();
            }
            else
            {
                radioButton1.Checked = false;
                sort = "DESC";
                Filtration();
            }
        }


        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >=0)
            {
                string Names = dataGridView1.Rows[rowIndex].Cells["Название"].Value.ToString();
                string sqlQuery = "select title,Type, Description, o.Name , Power,Price,Stock_balance,Photo from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture WHERE title='" + Names + "';";

                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();

                textBox2.Text = rdr["title"].ToString();
                comboBox4.Text = rdr["Name"].ToString();
                textBox3.Text = rdr["Description"].ToString();
                textBox4.Text = rdr["Power"].ToString();
                textBox5.Text = rdr["Price"].ToString();
                textBox6.Text = rdr["Stock_balance"].ToString();
                comboBox1.Text = rdr["Type"].ToString();
                try
                {
                    string photo = rdr["Photo"].ToString();
                    if (photo == "")
                    {
                        photo = "picture.png";
                    }
                    Image sketch = new Bitmap($@"{data.path}\{photo}");
                    pictureBox3.Image = sketch;
                }
                catch (Exception)
                {
                    string photo = rdr["Photo"].ToString();
                    photo = "picture.png";
                    Image sketch = new Bitmap($@"{data.path}\{photo}");
                    pictureBox3.Image = sketch;
                }
                button4.Enabled = true;
            }
            
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    if ((fileInfo.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                         fileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase)) &&
                        fileInfo.Length <= 2 * 1024 * 1024)
                    {
                        pictureBox3.Image = Image.FromFile(openFileDialog.FileName);
                        photoName = fileInfo.Name;
                        fullPath = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("Выберите файл JPG или PNG размером не более 2 Мб.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Пистолет")
            {
                TypeStr = "Пистолет";
            }
            else if (comboBox3.Text == "Револьвер")
            {
                TypeStr = "Револьвер";
            }
            else if (comboBox3.Text == "Винтовка")
            {
                TypeStr = "Винтовка";
            }
            else if (comboBox3.Text == "Патроны")
            {
                TypeStr = "Патроны";
            }
            else if (comboBox3.Text == "Балон")
            {
                TypeStr = "Балон";
            }
            else if (comboBox3.Text == "Тюнинг")
            {
                TypeStr = "Тюнинг";
            }
            else if (comboBox3.Text == "Аксессуар")
            {
                TypeStr = "Аксессуар";
            }
            Filtration();
        }
        void Filtration()
        {
            dataGridView1.Rows.Clear();

            using (MySqlConnection con = new MySqlConnection(data.conStr))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand($"select title, Type, Description, o.Name AS 'Производитель'," +
                    $" Power,Price,Stock_balance,Photo from products INNER JOIN `manufacture` AS `o` ON Manufacter = o.idManufacture WHERE Type = '{TypeStr}' AND `title` like '%{poisk}%' ORDER BY Price {sort};", con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j != 7)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            string photo = dt.Rows[i][j].ToString();
                            try
                            {
                                if (photo == "")
                                {
                                    photo = "picture.png";
                                }
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                            catch (Exception)
                            {
                                photo = "picture.png";
                                Image sketch = new Bitmap($@"{data.path}\{photo}");
                                dataGridView1.Rows[i].Cells[j].Value = sketch;
                            }
                        }
                    }
                }
            }
            photoName = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox3.Text = null;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox1.Text = "";
            viewTable();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var CellValue = dataGridView1.Rows[e.RowIndex].Cells["Stock"].Value;
                if (CellValue != null)
                {
                    string statusVIP = CellValue.ToString();
                    switch (statusVIP)
                    {
                        case "0":
                            e.CellStyle.BackColor = Color.Yellow;
                            break;
                        default:
                            e.CellStyle.BackColor = Color.White;
                            break;
                    }
                }
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
                            string strCmd = "DELETE FROM products WHERE ";
                            string strCon = data.conStr;
                            strCmd += "title='" + cell0 + "';";
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

                                        viewTable();
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
