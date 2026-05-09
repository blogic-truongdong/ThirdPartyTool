namespace ThirdPartyTool
{
    partial class Form1
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
            this.rjComboBox1 = new RJCodeAdvance.RJControls.RJComboBox();
            this.rjDatePicker1 = new RJCodeAdvance.RJControls.RJDatePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.DateTimeStartDate = new BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.customCombobox1 = new ThirdPartyTool.CustomControl.CustomCombobox();
            this.customDateTimePicker1 = new ThirdPartyTool.CustomControl.CustomDateTimePicker();
            this.customCombobox2 = new ThirdPartyTool.CustomControl.CustomCombobox();
            this.SuspendLayout();
            // 
            // rjComboBox1
            // 
            this.rjComboBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rjComboBox1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.rjComboBox1.BorderSize = 1;
            this.rjComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.rjComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rjComboBox1.ForeColor = System.Drawing.Color.DimGray;
            this.rjComboBox1.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.rjComboBox1.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.rjComboBox1.ListTextColor = System.Drawing.Color.DimGray;
            this.rjComboBox1.Location = new System.Drawing.Point(135, 121);
            this.rjComboBox1.MinimumSize = new System.Drawing.Size(200, 30);
            this.rjComboBox1.Name = "rjComboBox1";
            this.rjComboBox1.Padding = new System.Windows.Forms.Padding(1);
            this.rjComboBox1.Size = new System.Drawing.Size(200, 30);
            this.rjComboBox1.TabIndex = 0;
            this.rjComboBox1.Texts = "";
            // 
            // rjDatePicker1
            // 
            this.rjDatePicker1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.rjDatePicker1.BorderSize = 0;
            this.rjDatePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.rjDatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rjDatePicker1.Location = new System.Drawing.Point(224, 257);
            this.rjDatePicker1.MinimumSize = new System.Drawing.Size(4, 35);
            this.rjDatePicker1.Name = "rjDatePicker1";
            this.rjDatePicker1.Size = new System.Drawing.Size(200, 35);
            this.rjDatePicker1.SkinColor = System.Drawing.Color.MediumSlateBlue;
            this.rjDatePicker1.TabIndex = 1;
            this.rjDatePicker1.TextColor = System.Drawing.Color.White;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(423, 142);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.Value = new System.DateTime(2020, 10, 27, 0, 0, 0, 0);
            // 
            // DateTimeStartDate
            // 
            this.DateTimeStartDate.DateTimeValues = new System.DateTime(2015, 12, 14, 11, 10, 0, 0);
            this.DateTimeStartDate.Location = new System.Drawing.Point(182, 319);
            this.DateTimeStartDate.Name = "DateTimeStartDate";
            this.DateTimeStartDate.ShowCheckBox = true;
            this.DateTimeStartDate.ShowDate = true;
            this.DateTimeStartDate.Size = new System.Drawing.Size(452, 39);
            this.DateTimeStartDate.TabIndex = 15;
            this.DateTimeStartDate.WrongValues = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(385, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 16;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBox2.Location = new System.Drawing.Point(611, 259);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 18;
            // 
            // customCombobox1
            // 
            this.customCombobox1.Font = new System.Drawing.Font("Roboto", 12F);
            this.customCombobox1.Location = new System.Drawing.Point(195, 63);
            this.customCombobox1.Margin = new System.Windows.Forms.Padding(0);
            this.customCombobox1.Name = "customCombobox1";
            this.customCombobox1.Size = new System.Drawing.Size(183, 35);
            this.customCombobox1.TabIndex = 17;
            // 
            // customDateTimePicker1
            // 
            this.customDateTimePicker1.BackColor = System.Drawing.Color.White;
            this.customDateTimePicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customDateTimePicker1.Value = new System.DateTime(2026, 2, 22, 0, 0, 0, 0);
            this.customDateTimePicker1.Location = new System.Drawing.Point(451, 187);
            this.customDateTimePicker1.Name = "customDateTimePicker1";
            this.customDateTimePicker1.Size = new System.Drawing.Size(200, 40);
            this.customDateTimePicker1.TabIndex = 3;
            // 
            // customCombobox2
            // 
            this.customCombobox2.Location = new System.Drawing.Point(242, 188);
            this.customCombobox2.Name = "customCombobox2";
            this.customCombobox2.Size = new System.Drawing.Size(153, 35);
            this.customCombobox2.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.customCombobox2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.customCombobox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.DateTimeStartDate);
            this.Controls.Add(this.customDateTimePicker1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.rjDatePicker1);
            this.Controls.Add(this.rjComboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private RJCodeAdvance.RJControls.RJComboBox rjComboBox1;
        private RJCodeAdvance.RJControls.RJDatePicker rjDatePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private CustomControl.CustomDateTimePicker customDateTimePicker1;
        public BLogicSystems.POSBackOffice.Control.BUserControl.BDateTimePickerCustom DateTimeStartDate;
        private System.Windows.Forms.ComboBox comboBox1;
        private CustomControl.CustomCombobox customCombobox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private CustomControl.CustomCombobox customCombobox2;
    }
}