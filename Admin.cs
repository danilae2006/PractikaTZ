﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZEgorov.ShowForm;

namespace TZEgorov
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            InitializeIdleTimer();
            LoadIdleTimeout();
        }
        private Timer idleTimer;
        private int idleTimeout = Convert.ToInt32(Properties.Settings.Default.Time);

        #region Таймер
        private void LoadIdleTimeout()
        {
            if (int.TryParse(ConfigurationManager.AppSettings["IdleTimeout"], out int timeout))
            {
                idleTimeout = timeout * 1000;
            }
            else
            {
                idleTimeout = 30000;
            }
        }

        private void InitializeIdleTimer()
        {
            idleTimer = new Timer();
            idleTimer.Interval = idleTimeout;
            idleTimer.Tick += IdleTimer_Tick;
            idleTimer.Start();
            this.MouseMove += ResetIdleTimer;
            this.KeyPress += ResetIdleTimer;
            this.KeyDown += ResetIdleTimer;
            this.MouseClick += ResetIdleTimer;
        }

        private void ResetIdleTimer(object sender, EventArgs e)
        {
            idleTimer.Stop();
            idleTimer.Start();
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            idleTimer.Stop();
            this.Hide();

            using (var loginForm = new Form1())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        #endregion
        private void Admin_Load(object sender, EventArgs e)
        {

            this.MaximizeBox = false;
            this.MinimizeBox = false;
            label2.Text = $"{data.usrName}" + $" {data.usrSurname}" + $" {data.usrPatr}";
            if (data.role == "Продавец")
            {
                button4.Visible = false;
                this.Height = 585;
                button7.Location = new Point(18, 422);
                button6.Location = new Point(18, 484);
                this.Text = "Главное меню. Продавец";
            }
            else if (data.role == "Локальный")
            {
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button9.Visible = false;
                    button5.Visible = false;
                    button8.Visible = false;
                    button4.Visible = false;
                label2.Text = "Локальный админ";
                button7.Location = new Point(18, 159);
                button6.Location = new Point(18, 221);
                this.Height = 320;
                button10.Visible = true;
                button11.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Product viewadd = new Product();
            this.Visible = false;
            viewadd.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            this.Visible = false;
            order.ShowDialog();
            this.Close();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            this.Visible = false;
            users.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sevices services = new Sevices();
            this.Visible = false;
            services.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Services_performed services_Performed = new Services_performed();
            this.Visible = false;
            services_Performed.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            this.Visible = false;
            client.ShowDialog();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Manufacter manufacter = new Manufacter();
            this.Visible = false;
            manufacter.ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Rebuild rebuild = new Rebuild();
            this.Visible = false;
            rebuild.ShowDialog();
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ImportForm importForm = new ImportForm();
            this.Visible = false;
            importForm.ShowDialog();
            this.Close();
        }
    }
}
