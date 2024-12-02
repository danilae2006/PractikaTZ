namespace TZEgorov.AddForm
{
    partial class AddPerformed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPerformed));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.maskedBirth = new System.Windows.Forms.MaskedTextBox();
            this.pictureCalendar1 = new System.Windows.Forms.PictureBox();
            this.monthCalendarBirth = new System.Windows.Forms.MonthCalendar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCalendar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(163, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(196, 28);
            this.comboBox1.TabIndex = 107;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Red;
            this.button2.Location = new System.Drawing.Point(13, 167);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(346, 42);
            this.button2.TabIndex = 106;
            this.button2.Text = "Вернуться";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(13, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(346, 42);
            this.button1.TabIndex = 105;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 24);
            this.label3.TabIndex = 102;
            this.label3.Text = "Дата записи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 24);
            this.label2.TabIndex = 101;
            this.label2.Text = "Клиент";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 100;
            this.label1.Text = "Услуга";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(163, 48);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(170, 28);
            this.comboBox2.TabIndex = 108;
            this.comboBox2.DropDown += new System.EventHandler(this.comboBox2_DropDown);
            // 
            // maskedBirth
            // 
            this.maskedBirth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedBirth.Location = new System.Drawing.Point(163, 82);
            this.maskedBirth.Mask = "0000-00-00";
            this.maskedBirth.Name = "maskedBirth";
            this.maskedBirth.Size = new System.Drawing.Size(170, 26);
            this.maskedBirth.TabIndex = 109;
            this.maskedBirth.ValidatingType = typeof(System.DateTime);
            this.maskedBirth.TextChanged += new System.EventHandler(this.maskedBirth_TextChanged);
            this.maskedBirth.Leave += new System.EventHandler(this.maskedBirth_Leave);
            // 
            // pictureCalendar1
            // 
            this.pictureCalendar1.BackColor = System.Drawing.Color.White;
            this.pictureCalendar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureCalendar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureCalendar1.Image = ((System.Drawing.Image)(resources.GetObject("pictureCalendar1.Image")));
            this.pictureCalendar1.Location = new System.Drawing.Point(333, 82);
            this.pictureCalendar1.Name = "pictureCalendar1";
            this.pictureCalendar1.Size = new System.Drawing.Size(26, 26);
            this.pictureCalendar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCalendar1.TabIndex = 110;
            this.pictureCalendar1.TabStop = false;
            this.pictureCalendar1.Click += new System.EventHandler(this.pictureCalendar1_Click);
            // 
            // monthCalendarBirth
            // 
            this.monthCalendarBirth.Location = new System.Drawing.Point(195, 14);
            this.monthCalendarBirth.Name = "monthCalendarBirth";
            this.monthCalendarBirth.TabIndex = 111;
            this.monthCalendarBirth.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarBirth_DateChanged);
            this.monthCalendarBirth.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarBirth_DateSelected);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TZEgorov.Properties.Resources.add;
            this.pictureBox1.Location = new System.Drawing.Point(333, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 112;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // AddPerformed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(373, 219);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.monthCalendarBirth);
            this.Controls.Add(this.pictureCalendar1);
            this.Controls.Add(this.maskedBirth);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPerformed";
            this.Text = "Запись на услуги";
            this.Load += new System.EventHandler(this.AddPerformed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCalendar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.MaskedTextBox maskedBirth;
        private System.Windows.Forms.PictureBox pictureCalendar1;
        private System.Windows.Forms.MonthCalendar monthCalendarBirth;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}