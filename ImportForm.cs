using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        }
    }
}
