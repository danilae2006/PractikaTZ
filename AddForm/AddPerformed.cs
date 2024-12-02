using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.ShowForm;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TZEgorov.AddForm
{
    public partial class AddPerformed : Form
    {
        public AddPerformed()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        string connect = data.conStr;
        public int services;
        public int client;
        private void button2_Click(object sender, EventArgs e)
        {
            Services_performed services_Performed = new Services_performed();
            this.Visible = false;
            services_Performed.ShowDialog();
            this.Close();
        }

        private void maskedBirth_Leave(object sender, EventArgs e)
        {
            // Проверка корректности ввода даты
            if (DateTime.TryParseExact(maskedBirth.Text, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                // Задаём диапазон
                DateTime minDate = DateTime.Today;
                DateTime maxDate = new DateTime(2030, 1, 1);

                // Проверяем, входит ли дата в диапазон
                if (date >= minDate && date <= maxDate)
                {
                    monthCalendarBirth.SetDate(date); // Устанавливаем дату в календаре
                }
                else
                {
                    // Если дата за пределами диапазона
                    MessageBox.Show($"Введите дату в диапазоне от {minDate:yyyy.MM.dd} до {maxDate:yyyy.MM.dd}",
                                    "Некорректная дата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    maskedBirth.Focus();
                }
            }
            else
            {
                // Если формат даты некорректен
                MessageBox.Show("Введите корректную дату в формате дд.мм.гггг",
                                "Неверный формат даты", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskedBirth.Focus();
            }
        }

        private void maskedBirth_TextChanged(object sender, EventArgs e)
        {
            Text = DateTime.Now.ToString("yyyy.MM.dd");
        }

        private void pictureCalendar1_Click(object sender, EventArgs e)
        {
            monthCalendarBirth.Visible = !monthCalendarBirth.Visible;
        }

        private void monthCalendarBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            maskedBirth.Text = monthCalendarBirth.SelectionStart.ToString("yyyy.MM.dd");
            monthCalendarBirth.Visible = false; // Скрыть календарь после выбора даты
        }

        private void AddPerformed_Load(object sender, EventArgs e)
        {
            CustomizeMonthCalendar();
            comboBoxFill1();
            comboBoxFill2();
            maskedBirth.Enabled = false;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void comboBoxFill1()
        {
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = "select title from `services`";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                comboBox1.Items.Add(Rooms.Rows[i]["title"]);
            }
        }
        private void comboBoxFill2()
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
        private void CustomizeMonthCalendar()
        {
            // Убираем кнопку "Сегодня:" и выделение текущей даты
            monthCalendarBirth.ShowToday = false;
            monthCalendarBirth.ShowTodayCircle = false;

            // Ограничения диапазона дат
            monthCalendarBirth.MinDate = DateTime.Today;
            monthCalendarBirth.MaxDate = new DateTime(2030, 1, 1);

            // Скрываем календарь по умолчанию
            monthCalendarBirth.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Service = comboBox1.Text;
            string Client = comboBox2.Text;
            string Date = maskedBirth.Text;
            string[] words = Client.Split(' ');
            DataTable Rooms = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = coon;
                cmd.CommandText = $@"select idServices from `services` where title = '{Service}'";
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(Rooms);
            }

            for (int i = 0; i < Rooms.Rows.Count; i++)
            {
                services = Convert.ToInt32(Rooms.Rows[i]["idServices"]);
            }

            DataTable Rooms2 = new DataTable();
            using (MySqlConnection coon = new MySqlConnection(connect))
            {
                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = coon;
                cmd2.CommandText = $@"select idClient from `client` where surname = '{words[0]}' and name = '{words[1]}'";
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
                adapter2.Fill(Rooms2);
            }

            for (int i = 0; i < Rooms2.Rows.Count; i++)
            {
                client = Convert.ToInt32(Rooms2.Rows[i]["idClient"]);
            }

            string sqlQuery = $@"Insert Into `services_performed`
(idServices_performed,Client,Date,Status)
Values ({services},{client},'{maskedBirth.Text}','Новый')";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = connect;
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    MessageBox.Show("Клиент записан на услугу");

                }
                else
                {
                    MessageBox.Show("Клиент не записан на услугу");
                }
                comboBox1.Text = null;
                comboBox2.Text = null;
                maskedBirth.Text = null;
            }
        }

        private void monthCalendarBirth_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBoxFill2();
        }
    }
}
