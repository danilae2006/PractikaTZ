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

namespace TZEgorov
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Admin admin = new Admin();
                this.Visible = false;
                admin.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = comboBox1.Text;
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\bin\Debug\";
            OPF.Filter = "Файлы csv|*.csv";
            string FileName = string.Empty;
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                FileName = OPF.FileName;
                if (Path.GetFileNameWithoutExtension(FileName) != tableName)
                {
                    MessageBox.Show("Название файла не совпадает с названием таблицы. Пожалуйста, выберите правильный файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Файл не выбран");
                return;
            }
            string[] readText = File.ReadAllLines(FileName, Encoding.GetEncoding(1251));
            string[] titleField = readText[0].Split(';');
            string[] valField;
            string strCommand = string.Empty;
            int count;
            using (MySqlConnection con = new MySqlConnection(data.conStr))
            {
                con.Open();
                //MySqlCommand cmdClearTable = new MySqlCommand($"DELETE FROM `{tableName}`;", con);
                //cmdClearTable.ExecuteNonQuery();

                foreach (string str in readText.Skip(1).ToArray())
                {
                    valField = str.Split(';');

                    #region tableImport
                        switch (tableName)
                        {
                            case "manufacturer":
                                strCommand = $@"Insert Into `manufacture`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}')";
                                break;
                            case "order":
                                strCommand = $@"Insert Into `order`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}','{valField[4]}','{valField[5]}')";
                                break;
                            case "listofproducts":
                                MySqlCommand cmdCheckTableProduct = new MySqlCommand("SELECT COUNT(*) FROM products;", con);
                                count = cmdCheckTableProduct.ExecuteNonQuery();
                                if (count > 0)
                                {
                                    strCommand = $@"Insert Into `listofproducts`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}')";
                                }
                                else
                                {
                                    MessageBox.Show("Сначала заполните таблицу Products");
                                    return;
                                }

                                break;
                            case "products":
                                strCommand = $@"Insert Into `products`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}','{valField[4]}','{valField[5]}','{valField[6]}','{valField[7]}','{valField[8]}')";
                                break;
                            case "services":
                                strCommand = $@"Insert Into `services`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}','{valField[4]}')";
                                break;
                            case "role":
                                strCommand = $@"Insert Into `role`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}')";
                                break;
                            case "client":

                                strCommand = $@"Insert Into `client`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}')";
                                break;
                            case "user":
                                strCommand = $@"Insert Into `user`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}','{valField[4]}','{valField[5]}','{valField[6]}','{valField[7]}','{valField[8]}')";
                                break;
                            case "services_performed":
                                strCommand = $@"Insert Into `services_performed`({String.Join(",", titleField)}) VALUES(
                                '{valField[0]}','{valField[1]}','{valField[2]}','{valField[3]}')";
                                break;
                        }

                    #endregion
                    MySqlCommand cmd = new MySqlCommand(strCommand, con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                }
                MessageBox.Show($"Данные импортированны в таблицу {tableName}");
                con.Close();
            }
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                using (MySqlConnection con = new MySqlConnection(data.conStr))
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("Show Tables", con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0]);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.Text != null;
        }
    }
}
