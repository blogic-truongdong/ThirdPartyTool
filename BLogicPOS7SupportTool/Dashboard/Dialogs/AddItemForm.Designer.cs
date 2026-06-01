namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class AddItemForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lvItems = new System.Windows.Forms.ListView();
            this.colItemName = new System.Windows.Forms.ColumnHeader();
            this.colLookup = new System.Windows.Forms.ColumnHeader();
            this.colPrice = new System.Windows.Forms.ColumnHeader();
            this.imgRowHeight = new System.Windows.Forms.ImageList(this.components);
            this.pnlEdit = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEditHeader = new System.Windows.Forms.Label();
            this.pnlEditDivider = new System.Windows.Forms.Panel();
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitleBar
            //
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.pnlTitleBar.Controls.Add(this.btnCloseForm);
            this.pnlTitleBar.Controls.Add(this.lblFormTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(560, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(520, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(40, 40);
            this.btnCloseForm.TabIndex = 1;
            this.btnCloseForm.Text = "X";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            //
            // lblFormTitle
            //
            this.lblFormTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(0, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblFormTitle.Size = new System.Drawing.Size(520, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Add Item";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 548);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(560, 52);
            this.pnlFooter.TabIndex = 2;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(444, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(560, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lvItems);
            this.pnlContent.Controls.Add(this.pnlEdit);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12, 12, 12, 0);
            this.pnlContent.Size = new System.Drawing.Size(560, 508);
            this.pnlContent.TabIndex = 1;
            //
            // lvItems
            //
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colItemName,
            this.colLookup,
            this.colPrice});
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lvItems.FullRowSelect = true;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(12, 12);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(536, 336);
            this.lvItems.SmallImageList = this.imgRowHeight;
            this.lvItems.TabIndex = 0;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            //
            // colItemName
            //
            this.colItemName.Text = "ITEM NAME";
            this.colItemName.Width = 250;
            //
            // colLookup
            //
            this.colLookup.Text = "LOOKUP";
            this.colLookup.Width = 120;
            //
            // colPrice
            //
            this.colPrice.Text = "PRICE";
            this.colPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPrice.Width = 100;
            //
            // imgRowHeight
            //
            this.imgRowHeight.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgRowHeight.ImageSize = new System.Drawing.Size(1, 28);
            this.imgRowHeight.TransparentColor = System.Drawing.Color.Transparent;
            //
            // pnlEdit
            //
            this.pnlEdit.BackColor = System.Drawing.Color.White;
            this.pnlEdit.Controls.Add(this.btnAdd);
            this.pnlEdit.Controls.Add(this.txtQty);
            this.pnlEdit.Controls.Add(this.lblQty);
            this.pnlEdit.Controls.Add(this.txtPrice);
            this.pnlEdit.Controls.Add(this.lblPrice);
            this.pnlEdit.Controls.Add(this.txtName);
            this.pnlEdit.Controls.Add(this.lblName);
            this.pnlEdit.Controls.Add(this.lblEditHeader);
            this.pnlEdit.Controls.Add(this.pnlEditDivider);
            this.pnlEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEdit.Location = new System.Drawing.Point(12, 348);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(536, 160);
            this.pnlEdit.TabIndex = 1;
            //
            // btnAdd
            //
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(424, 118);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            //
            // txtQty
            //
            this.txtQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQty.Location = new System.Drawing.Point(300, 78);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(224, 25);
            this.txtQty.TabIndex = 3;
            this.txtQty.Text = "";
            //
            // lblQty
            //
            this.lblQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQty.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblQty.Location = new System.Drawing.Point(298, 60);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(80, 15);
            this.lblQty.TabIndex = 90;
            this.lblQty.Text = "QUANTITY";
            //
            // txtPrice
            //
            this.txtPrice.BackColor = System.Drawing.Color.White;
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrice.Location = new System.Drawing.Point(12, 78);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(250, 25);
            this.txtPrice.TabIndex = 2;
            this.txtPrice.Text = "";
            //
            // lblPrice
            //
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblPrice.Location = new System.Drawing.Point(10, 60);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(90, 15);
            this.lblPrice.TabIndex = 91;
            this.lblPrice.Text = "PRICE (USD)";
            //
            // txtName
            //
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(12, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(512, 25);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "";
            //
            // lblName
            //
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.lblName.Location = new System.Drawing.Point(10, 12);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(90, 15);
            this.lblName.TabIndex = 92;
            this.lblName.Text = "ITEM NAME";
            //
            // lblEditHeader
            //
            this.lblEditHeader.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEditHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(87)))), ((int)(((byte)(208)))));
            this.lblEditHeader.Location = new System.Drawing.Point(10, -20);
            this.lblEditHeader.Name = "lblEditHeader";
            this.lblEditHeader.Size = new System.Drawing.Size(400, 17);
            this.lblEditHeader.TabIndex = 93;
            this.lblEditHeader.Text = "";
            this.lblEditHeader.Visible = false;
            //
            // pnlEditDivider
            //
            this.pnlEditDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlEditDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEditDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlEditDivider.Name = "pnlEditDivider";
            this.pnlEditDivider.Size = new System.Drawing.Size(536, 1);
            this.pnlEditDivider.TabIndex = 0;
            //
            // AddItemForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(560, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader colItemName;
        private System.Windows.Forms.ColumnHeader colLookup;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ImageList imgRowHeight;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Panel pnlEditDivider;
        private System.Windows.Forms.Label lblEditHeader;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnAdd;
    }
}
