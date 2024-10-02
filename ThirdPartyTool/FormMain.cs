using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.Load += FormMain_Load;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btnAdjustipFile_0_0.Click += BtnAdjustipFile_0_0_Click;
        }

        private void BtnAdjustipFile_0_0_Click(object sender, EventArgs e)
        {
            UpdateTipForSaleReceiptFile_0_0 form  = new UpdateTipForSaleReceiptFile_0_0();
            form.ShowDialog();  
        }
    }
}
