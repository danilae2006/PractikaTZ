using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace TZEgorov
{
    public partial class Rebuild : Form
    {
        public Rebuild()
        {
            InitializeComponent();
        }
        string conString = $@"host=127.0.0.1;uid=root;pwd=root;";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string backupPath = "Backup\\Structure.sql";
                string databaseName = "weaponshop";
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();
                    MySqlCommand cmdDrop = new MySqlCommand($"DROP DATABASE IF EXISTS `{databaseName}`;", con);
                    cmdDrop.ExecuteNonQuery();

                    MySqlCommand cmdCreate = new MySqlCommand($"CREATE DATABASE `{databaseName}`;", con);
                    cmdCreate.ExecuteNonQuery();

                    MySqlCommand cmdUse = new MySqlCommand($"USE `{databaseName}`;", con);
                    cmdUse.ExecuteNonQuery();
                    string script = File.ReadAllText(backupPath);
                    MySqlScript sqlScript = new MySqlScript(con, script);
                    sqlScript.Execute();

                    con.Close();
                }
                MessageBox.Show("Востановление прошло успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string backupPath = "Backup\\DataAndStructure.sql";
                string databaseName = "weaponshop";
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    con.Open();

                    MySqlCommand cmdUse = new MySqlCommand($"USE `{databaseName}`;", con);
                    cmdUse.ExecuteNonQuery();
                    string script = File.ReadAllText(backupPath);
                    MySqlScript sqlScript = new MySqlScript(con, script);
                    sqlScript.Execute();

                    con.Close();
                }
                MessageBox.Show("Востановление прошло успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            this.Visible = false;
            admin.ShowDialog();
            this.Close();
        }
    }
}
