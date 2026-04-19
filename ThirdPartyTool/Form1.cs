using System.Windows.Forms;

namespace ThirdPartyTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            customDateTimePicker1.DateTimeValues = System.DateTime.Now;
        }
    }
}
