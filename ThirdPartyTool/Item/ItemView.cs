using System;
using System.Windows.Forms;

namespace ThirdPartyTool.Item
{
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();

            btnSyncItemType.Click += BtnSyncItemType_Click;

            this.Load += ItemView_Load;
        }

        private void ItemView_Load(object sender, EventArgs e)
        {

        }

        private void BtnSyncItemType_Click(object sender, EventArgs e)
        {
            reSyncItemTypeView1.Visible = true;
            reSyncItemTypeView1.BringToFront();
        }
    }
}
