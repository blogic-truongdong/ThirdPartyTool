using System.Windows.Forms;
using ThirdPartyTool.CustomControl;

namespace ThirdPartyTool.SaleReceipt
{
    partial class FinishTicketWithPaymentDetailForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.paymentTime = new System.Windows.Forms.DateTimePicker();
            this.paymentDate = new System.Windows.Forms.DateTimePicker();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.txtCardType = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReferenceNumber = new ThirdPartyTool.CustomControl.IntergeTextBox();
            this.txtExpiryDate = new ThirdPartyTool.CustomControl.IntergeTextBox();
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlBatchID = new System.Windows.Forms.Panel();
            this.btnBrowseBatch = new RJCodeAdvance.RJControls.RJButton();
            this.txtBatchID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pnlPaymentMethod = new System.Windows.Forms.Panel();
            this.cmbPaymentMethod = new RJCodeAdvance.RJControls.RJComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTotalAmount_Base = new System.Windows.Forms.RadioButton();
            this.txtTotalAmount_Include_SVCFee = new System.Windows.Forms.RadioButton();
            this.txtTotalAmount_NoneTax_Base = new System.Windows.Forms.RadioButton();
            this.txtTotalAmount_Include_CashDiscount = new System.Windows.Forms.RadioButton();
            this.txtTotalAmount_NoneTax_Include_SVCFee = new System.Windows.Forms.RadioButton();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTotalAmount_NoneTax_Include_CashDiscount = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtGratuity = new ThirdPartyTool.CustomControl.NumericTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOK = new RJCodeAdvance.RJControls.RJButton();
            this.btnCancel = new RJCodeAdvance.RJControls.RJButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pnlBatchID.SuspendLayout();
            this.pnlPaymentMethod.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.pnlBatchID);
            this.panel1.Controls.Add(this.pnlPaymentMethod);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 733);
            this.panel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtNote);
            this.panel7.Controls.Add(this.label19);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 596);
            this.panel7.Margin = new System.Windows.Forms.Padding(0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(25, 0, 25, 0);
            this.panel7.Size = new System.Drawing.Size(684, 67);
            this.panel7.TabIndex = 17;
            // 
            // txtNote
            // 
            this.txtNote.BackColor = System.Drawing.SystemColors.Window;
            this.txtNote.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNote.Location = new System.Drawing.Point(74, 10);
            this.txtNote.Margin = new System.Windows.Forms.Padding(0);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(590, 50);
            this.txtNote.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(20, 10);
            this.label19.Margin = new System.Windows.Forms.Padding(0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 30);
            this.label19.TabIndex = 1;
            this.label19.Text = "Note";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.paymentTime);
            this.panel5.Controls.Add(this.paymentDate);
            this.panel5.Controls.Add(this.txtCardNumber);
            this.panel5.Controls.Add(this.txtAuthCode);
            this.panel5.Controls.Add(this.txtCardType);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.txtReferenceNumber);
            this.panel5.Controls.Add(this.txtExpiryDate);
            this.panel5.Controls.Add(this.txtCardName);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label10);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 359);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panel5.Size = new System.Drawing.Size(684, 237);
            this.panel5.TabIndex = 15;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label24);
            this.panel6.Location = new System.Drawing.Point(473, 201);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(46, 27);
            this.panel6.TabIndex = 21;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.DimGray;
            this.label24.Dock = System.Windows.Forms.DockStyle.Left;
            this.label24.Font = new System.Drawing.Font("Roboto", 12F);
            this.label24.Location = new System.Drawing.Point(0, 0);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(1, 27);
            this.label24.TabIndex = 12;
            this.label24.Text = "Auth Code";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // paymentTime
            // 
            this.paymentTime.Font = new System.Drawing.Font("Roboto", 12F);
            this.paymentTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.paymentTime.Location = new System.Drawing.Point(377, 201);
            this.paymentTime.Name = "paymentTime";
            this.paymentTime.Size = new System.Drawing.Size(133, 27);
            this.paymentTime.TabIndex = 20;
            // 
            // paymentDate
            // 
            this.paymentDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.paymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.paymentDate.Location = new System.Drawing.Point(244, 201);
            this.paymentDate.Name = "paymentDate";
            this.paymentDate.Size = new System.Drawing.Size(127, 27);
            this.paymentDate.TabIndex = 19;
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtCardNumber.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtCardNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCardNumber.Location = new System.Drawing.Point(464, 70);
            this.txtCardNumber.Margin = new System.Windows.Forms.Padding(0);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(200, 27);
            this.txtCardNumber.TabIndex = 15;
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtAuthCode.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtAuthCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAuthCode.Location = new System.Drawing.Point(244, 134);
            this.txtAuthCode.Margin = new System.Windows.Forms.Padding(0);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(200, 27);
            this.txtAuthCode.TabIndex = 17;
            // 
            // txtCardType
            // 
            this.txtCardType.BackColor = System.Drawing.SystemColors.Window;
            this.txtCardType.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtCardType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCardType.Location = new System.Drawing.Point(244, 70);
            this.txtCardType.Margin = new System.Windows.Forms.Padding(0);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.Size = new System.Drawing.Size(200, 27);
            this.txtCardType.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Roboto", 12F);
            this.label13.Location = new System.Drawing.Point(460, 40);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 30);
            this.label13.TabIndex = 11;
            this.label13.Text = "Card Number";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReferenceNumber
            // 
            this.txtReferenceNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtReferenceNumber.DefaultValue = "";
            this.txtReferenceNumber.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtReferenceNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtReferenceNumber.Location = new System.Drawing.Point(24, 198);
            this.txtReferenceNumber.Margin = new System.Windows.Forms.Padding(0);
            this.txtReferenceNumber.Name = "txtReferenceNumber";
            this.txtReferenceNumber.Size = new System.Drawing.Size(200, 27);
            this.txtReferenceNumber.TabIndex = 16;
            this.txtReferenceNumber.Value = 0;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtExpiryDate.DefaultValue = "";
            this.txtExpiryDate.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtExpiryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtExpiryDate.Location = new System.Drawing.Point(24, 134);
            this.txtExpiryDate.Margin = new System.Windows.Forms.Padding(0);
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.Size = new System.Drawing.Size(200, 27);
            this.txtExpiryDate.TabIndex = 16;
            this.txtExpiryDate.Value = 0;
            // 
            // txtCardName
            // 
            this.txtCardName.BackColor = System.Drawing.SystemColors.Window;
            this.txtCardName.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtCardName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCardName.Location = new System.Drawing.Point(24, 70);
            this.txtCardName.Margin = new System.Windows.Forms.Padding(0);
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(200, 27);
            this.txtCardName.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Roboto", 12F);
            this.label15.Location = new System.Drawing.Point(240, 104);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 30);
            this.label15.TabIndex = 11;
            this.label15.Text = "Auth Code";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Roboto", 12F);
            this.label12.Location = new System.Drawing.Point(240, 40);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(150, 30);
            this.label12.TabIndex = 11;
            this.label12.Text = "Card Type";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Roboto", 12F);
            this.label23.Location = new System.Drawing.Point(240, 168);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(150, 30);
            this.label23.TabIndex = 11;
            this.label23.Text = "Payment Date";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Roboto", 12F);
            this.label25.Location = new System.Drawing.Point(20, 168);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(150, 30);
            this.label25.TabIndex = 11;
            this.label25.Text = "Reference Number";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Roboto", 12F);
            this.label14.Location = new System.Drawing.Point(20, 104);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 30);
            this.label14.TabIndex = 11;
            this.label14.Text = "Expiry Date";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Roboto", 12F);
            this.label10.Location = new System.Drawing.Point(20, 40);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 30);
            this.label10.TabIndex = 11;
            this.label10.Text = "Card Name";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(20, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(190, 30);
            this.label6.TabIndex = 1;
            this.label6.Text = "Card Infomation";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Gray;
            this.label11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label11.Location = new System.Drawing.Point(20, 236);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(644, 1);
            this.label11.TabIndex = 10;
            this.label11.Text = "label11";
            // 
            // pnlBatchID
            // 
            this.pnlBatchID.Controls.Add(this.btnBrowseBatch);
            this.pnlBatchID.Controls.Add(this.txtBatchID);
            this.pnlBatchID.Controls.Add(this.label17);
            this.pnlBatchID.Controls.Add(this.label18);
            this.pnlBatchID.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBatchID.Location = new System.Drawing.Point(0, 305);
            this.pnlBatchID.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBatchID.Name = "pnlBatchID";
            this.pnlBatchID.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.pnlBatchID.Size = new System.Drawing.Size(684, 54);
            this.pnlBatchID.TabIndex = 16;
            // 
            // btnBrowseBatch
            // 
            this.btnBrowseBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseBatch.BackColor = System.Drawing.Color.White;
            this.btnBrowseBatch.BackgroundColor = System.Drawing.Color.White;
            this.btnBrowseBatch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnBrowseBatch.BorderRadius = 4;
            this.btnBrowseBatch.BorderSize = 1;
            this.btnBrowseBatch.FlatAppearance.BorderSize = 0;
            this.btnBrowseBatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseBatch.Font = new System.Drawing.Font("BLogic", 12F);
            this.btnBrowseBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnBrowseBatch.Location = new System.Drawing.Point(301, 9);
            this.btnBrowseBatch.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowseBatch.Name = "btnBrowseBatch";
            this.btnBrowseBatch.Size = new System.Drawing.Size(96, 34);
            this.btnBrowseBatch.TabIndex = 9;
            this.btnBrowseBatch.Text = "Browse";
            this.btnBrowseBatch.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnBrowseBatch.UseVisualStyleBackColor = false;
            // 
            // txtBatchID
            // 
            this.txtBatchID.BackColor = System.Drawing.SystemColors.Window;
            this.txtBatchID.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtBatchID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBatchID.Location = new System.Drawing.Point(90, 13);
            this.txtBatchID.Margin = new System.Windows.Forms.Padding(0);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.ReadOnly = true;
            this.txtBatchID.Size = new System.Drawing.Size(200, 27);
            this.txtBatchID.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(20, 15);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 30);
            this.label17.TabIndex = 1;
            this.label17.Text = "Batch:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Gray;
            this.label18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label18.Location = new System.Drawing.Point(20, 53);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(644, 1);
            this.label18.TabIndex = 10;
            this.label18.Text = "label18";
            // 
            // pnlPaymentMethod
            // 
            this.pnlPaymentMethod.Controls.Add(this.cmbPaymentMethod);
            this.pnlPaymentMethod.Controls.Add(this.label4);
            this.pnlPaymentMethod.Controls.Add(this.label8);
            this.pnlPaymentMethod.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPaymentMethod.Location = new System.Drawing.Point(0, 255);
            this.pnlPaymentMethod.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPaymentMethod.Name = "pnlPaymentMethod";
            this.pnlPaymentMethod.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.pnlPaymentMethod.Size = new System.Drawing.Size(684, 50);
            this.pnlPaymentMethod.TabIndex = 14;
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.BackColor = System.Drawing.Color.White;
            this.cmbPaymentMethod.BorderColor = System.Drawing.Color.Gray;
            this.cmbPaymentMethod.BorderSize = 1;
            this.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentMethod.Font = new System.Drawing.Font("Roboto", 12F);
            this.cmbPaymentMethod.ForeColor = System.Drawing.Color.Black;
            this.cmbPaymentMethod.IconColor = System.Drawing.Color.Black;
            this.cmbPaymentMethod.ListBackColor = System.Drawing.Color.White;
            this.cmbPaymentMethod.ListTextColor = System.Drawing.Color.Black;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(168, 10);
            this.cmbPaymentMethod.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPaymentMethod.MinimumSize = new System.Drawing.Size(200, 30);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Padding = new System.Windows.Forms.Padding(1);
            this.cmbPaymentMethod.Size = new System.Drawing.Size(250, 30);
            this.cmbPaymentMethod.TabIndex = 7;
            this.cmbPaymentMethod.Texts = "";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(20, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "Payment Method:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(20, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(644, 1);
            this.label8.TabIndex = 10;
            this.label8.Text = "label8";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtTotalAmount_Base);
            this.panel3.Controls.Add(this.txtTotalAmount_Include_SVCFee);
            this.panel3.Controls.Add(this.txtTotalAmount_NoneTax_Base);
            this.panel3.Controls.Add(this.txtTotalAmount_Include_CashDiscount);
            this.panel3.Controls.Add(this.txtTotalAmount_NoneTax_Include_SVCFee);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.txtTotalAmount_NoneTax_Include_CashDiscount);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.txtGratuity);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 100);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.panel3.Size = new System.Drawing.Size(684, 155);
            this.panel3.TabIndex = 13;
            // 
            // txtTotalAmount_Base
            // 
            this.txtTotalAmount_Base.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_Base.Location = new System.Drawing.Point(24, 69);
            this.txtTotalAmount_Base.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_Base.Name = "txtTotalAmount_Base";
            this.txtTotalAmount_Base.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_Base.TabIndex = 1;
            this.txtTotalAmount_Base.TabStop = true;
            this.txtTotalAmount_Base.Text = "$9999.99";
            this.txtTotalAmount_Base.UseVisualStyleBackColor = true;
            // 
            // txtTotalAmount_Include_SVCFee
            // 
            this.txtTotalAmount_Include_SVCFee.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_Include_SVCFee.Location = new System.Drawing.Point(129, 69);
            this.txtTotalAmount_Include_SVCFee.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_Include_SVCFee.Name = "txtTotalAmount_Include_SVCFee";
            this.txtTotalAmount_Include_SVCFee.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_Include_SVCFee.TabIndex = 2;
            this.txtTotalAmount_Include_SVCFee.TabStop = true;
            this.txtTotalAmount_Include_SVCFee.Text = "$9999.99";
            this.txtTotalAmount_Include_SVCFee.UseVisualStyleBackColor = true;
            // 
            // txtTotalAmount_NoneTax_Base
            // 
            this.txtTotalAmount_NoneTax_Base.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_NoneTax_Base.Location = new System.Drawing.Point(363, 69);
            this.txtTotalAmount_NoneTax_Base.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_NoneTax_Base.Name = "txtTotalAmount_NoneTax_Base";
            this.txtTotalAmount_NoneTax_Base.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_NoneTax_Base.TabIndex = 4;
            this.txtTotalAmount_NoneTax_Base.TabStop = true;
            this.txtTotalAmount_NoneTax_Base.Text = "$9999.99";
            this.txtTotalAmount_NoneTax_Base.UseVisualStyleBackColor = true;
            // 
            // txtTotalAmount_Include_CashDiscount
            // 
            this.txtTotalAmount_Include_CashDiscount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_Include_CashDiscount.Location = new System.Drawing.Point(234, 69);
            this.txtTotalAmount_Include_CashDiscount.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_Include_CashDiscount.Name = "txtTotalAmount_Include_CashDiscount";
            this.txtTotalAmount_Include_CashDiscount.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_Include_CashDiscount.TabIndex = 3;
            this.txtTotalAmount_Include_CashDiscount.TabStop = true;
            this.txtTotalAmount_Include_CashDiscount.Text = "$9999.99";
            this.txtTotalAmount_Include_CashDiscount.UseVisualStyleBackColor = true;
            // 
            // txtTotalAmount_NoneTax_Include_SVCFee
            // 
            this.txtTotalAmount_NoneTax_Include_SVCFee.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_NoneTax_Include_SVCFee.Location = new System.Drawing.Point(468, 69);
            this.txtTotalAmount_NoneTax_Include_SVCFee.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_NoneTax_Include_SVCFee.Name = "txtTotalAmount_NoneTax_Include_SVCFee";
            this.txtTotalAmount_NoneTax_Include_SVCFee.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_NoneTax_Include_SVCFee.TabIndex = 5;
            this.txtTotalAmount_NoneTax_Include_SVCFee.TabStop = true;
            this.txtTotalAmount_NoneTax_Include_SVCFee.Text = "$9999.99";
            this.txtTotalAmount_NoneTax_Include_SVCFee.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Roboto", 12F);
            this.label22.Location = new System.Drawing.Point(359, 45);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(192, 19);
            this.label22.TabIndex = 17;
            this.label22.Text = "Final Total ( Exclude Tax )";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalAmount_NoneTax_Include_CashDiscount
            // 
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Location = new System.Drawing.Point(573, 69);
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Margin = new System.Windows.Forms.Padding(0);
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Name = "txtTotalAmount_NoneTax_Include_CashDiscount";
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Size = new System.Drawing.Size(100, 30);
            this.txtTotalAmount_NoneTax_Include_CashDiscount.TabIndex = 6;
            this.txtTotalAmount_NoneTax_Include_CashDiscount.TabStop = true;
            this.txtTotalAmount_NoneTax_Include_CashDiscount.Text = "$9999.99";
            this.txtTotalAmount_NoneTax_Include_CashDiscount.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Location = new System.Drawing.Point(350, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(330, 53);
            this.panel8.TabIndex = 16;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Roboto", 12F);
            this.label21.Location = new System.Drawing.Point(20, 45);
            this.label21.Margin = new System.Windows.Forms.Padding(0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(188, 19);
            this.label21.TabIndex = 15;
            this.label21.Text = "Final Total ( Include Tax )";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(11, 55);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(330, 53);
            this.panel4.TabIndex = 13;
            // 
            // txtGratuity
            // 
            this.txtGratuity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtGratuity.Font = new System.Drawing.Font("Roboto", 12F);
            this.txtGratuity.FormatSpecifier = "c2";
            this.txtGratuity.Location = new System.Drawing.Point(93, 117);
            this.txtGratuity.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.txtGratuity.Name = "txtGratuity";
            this.txtGratuity.Size = new System.Drawing.Size(150, 27);
            this.txtGratuity.TabIndex = 12;
            this.txtGratuity.Text = "0.00";
            this.txtGratuity.Value = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(20, 115);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 30);
            this.label20.TabIndex = 11;
            this.label20.Text = "Gratuity";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(20, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(486, 30);
            this.label7.TabIndex = 1;
            this.label7.Text = "What should be the final total to close this ticket?";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Gray;
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Location = new System.Drawing.Point(20, 154);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(644, 1);
            this.label9.TabIndex = 10;
            this.label9.Text = "label9";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(684, 50);
            this.panel2.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ticket #";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Roboto", 12F);
            this.label3.Location = new System.Drawing.Point(90, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ticket #";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(684, 1);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnOK.BorderColor = System.Drawing.Color.Gray;
            this.btnOK.BorderRadius = 8;
            this.btnOK.BorderSize = 1;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(516, 668);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 50);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "Finish Ticket";
            this.btnOK.TextColor = System.Drawing.Color.White;
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundColor = System.Drawing.Color.White;
            this.btnCancel.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("BLogic", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(356, 668);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 50);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.Black;
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(684, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Finish Ticket";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FinishTicketWithPaymentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 733);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FinishTicketWithPaymentDetailForm";
            this.Text = "FinishSaleReceiptForm";
            this.panel1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.pnlBatchID.ResumeLayout(false);
            this.pnlBatchID.PerformLayout();
            this.pnlPaymentMethod.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton txtTotalAmount_Base;
        private RJCodeAdvance.RJControls.RJButton btnOK;
        private RJCodeAdvance.RJControls.RJButton btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton txtTotalAmount_Include_SVCFee;
        private System.Windows.Forms.Panel pnlPaymentMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private TextBox txtCardName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private RJCodeAdvance.RJControls.RJComboBox cmbPaymentMethod;
        private TextBox txtCardNumber;
        private TextBox txtAuthCode;
        private TextBox txtCardType;
        private System.Windows.Forms.Label label13;
        private IntergeTextBox txtExpiryDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel pnlBatchID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton txtTotalAmount_Include_CashDiscount;
        private RJCodeAdvance.RJControls.RJButton btnBrowseBatch;
        private TextBox txtBatchID;
        private System.Windows.Forms.Panel panel7;
        private TextBox txtNote;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton txtTotalAmount_NoneTax_Include_CashDiscount;
        private System.Windows.Forms.RadioButton txtTotalAmount_NoneTax_Include_SVCFee;
        private System.Windows.Forms.RadioButton txtTotalAmount_NoneTax_Base;
        private CustomControl.NumericTextBox txtGratuity;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.DateTimePicker paymentDate;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker paymentTime;
        private IntergeTextBox txtReferenceNumber;
        private System.Windows.Forms.Label label25;
    }
}