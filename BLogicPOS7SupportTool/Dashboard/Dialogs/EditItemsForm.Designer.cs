namespace BLogicPOS7SupportTool.Dashboard.Dialogs
{
    partial class EditItemsForm
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
            this.btnDone = new System.Windows.Forms.Button();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lvItems = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colPrice = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.colTax = new System.Windows.Forms.ColumnHeader();
            this.colDiscount = new System.Windows.Forms.ColumnHeader();
            this.colTotal = new System.Windows.Forms.ColumnHeader();
            this.colEdit = new System.Windows.Forms.ColumnHeader();
            this.imgRowHeight = new System.Windows.Forms.ImageList(this.components);
            this.pnlTitleBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
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
            this.pnlTitleBar.Size = new System.Drawing.Size(640, 40);
            this.pnlTitleBar.TabIndex = 0;
            //
            // btnCloseForm
            //
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(600, 0);
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
            this.lblFormTitle.Size = new System.Drawing.Size(600, 40);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Edit Items";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnDone);
            this.pnlFooter.Controls.Add(this.pnlFooterDivider);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 508);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(640, 52);
            this.pnlFooter.TabIndex = 2;
            //
            // btnDone
            //
            this.btnDone.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDone.Location = new System.Drawing.Point(524, 12);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(100, 28);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "Close";
            this.btnDone.UseVisualStyleBackColor = true;
            //
            // pnlFooterDivider
            //
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(640, 1);
            this.pnlFooterDivider.TabIndex = 0;
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lvItems);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 40);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(12);
            this.pnlContent.Size = new System.Drawing.Size(640, 468);
            this.pnlContent.TabIndex = 1;
            //
            // lvItems
            //
            this.lvItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPrice,
            this.colQty,
            this.colTax,
            this.colDiscount,
            this.colTotal,
            this.colEdit});
            this.lvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItems.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lvItems.FullRowSelect = true;
            this.lvItems.HideSelection = false;
            this.lvItems.Location = new System.Drawing.Point(12, 12);
            this.lvItems.MultiSelect = false;
            this.lvItems.Name = "lvItems";
            this.lvItems.Size = new System.Drawing.Size(616, 444);
            this.lvItems.SmallImageList = this.imgRowHeight;
            this.lvItems.TabIndex = 0;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.Details;
            //
            // colName
            //
            this.colName.Text = "ITEM NAME";
            this.colName.Width = 190;
            //
            // colPrice
            //
            this.colPrice.Text = "PRICE";
            this.colPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPrice.Width = 80;
            //
            // colQty
            //
            this.colQty.Text = "QTY";
            this.colQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colQty.Width = 50;
            //
            // colTax
            //
            this.colTax.Text = "TAX";
            this.colTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colTax.Width = 70;
            //
            // colDiscount
            //
            this.colDiscount.Text = "DISCOUNT";
            this.colDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDiscount.Width = 90;
            //
            // colTotal
            //
            this.colTotal.Text = "TOTAL";
            this.colTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colTotal.Width = 90;
            //
            // colEdit
            //
            this.colEdit.Text = "";
            this.colEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colEdit.Width = 40;
            //
            // imgRowHeight
            //
            this.imgRowHeight.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgRowHeight.ImageSize = new System.Drawing.Size(1, 32);
            this.imgRowHeight.TransparentColor = System.Drawing.Color.Transparent;
            //
            // EditItemsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditItemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPrice;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.ColumnHeader colTax;
        private System.Windows.Forms.ColumnHeader colDiscount;
        private System.Windows.Forms.ColumnHeader colTotal;
        private System.Windows.Forms.ColumnHeader colEdit;
        private System.Windows.Forms.ImageList imgRowHeight;
    }
}
