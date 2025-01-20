using System.Drawing;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CommonLabel : Label
    {
        public CommonLabel()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Margin = new Padding(5);
            this.Size = new Size(200, 30);
            this.Font = new Font("Roboto", 12, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
