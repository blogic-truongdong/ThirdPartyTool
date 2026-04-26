using BLogicSystems.XPos.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdPartyTool.CustomControl
{
    public partial class CustomYearCalendar : UserControl
    {

        public event EventHandler<int> DoneSelectYearEvent;

        public CustomYearCalendar()
        {
            InitializeComponent();

            btn1.Click += BtnYear_Click;
            btn2.Click += BtnYear_Click;
            btn3.Click += BtnYear_Click;
            btn4.Click += BtnYear_Click;
            btn5.Click += BtnYear_Click;
            btn6.Click += BtnYear_Click;
            btn7.Click += BtnYear_Click;
            btn8.Click += BtnYear_Click;
            btn9.Click += BtnYear_Click;
            btn10.Click += BtnYear_Click;
            btn11.Click += BtnYear_Click;
            btn12.Click += BtnYear_Click;
        }

        private void BtnYear_Click(object sender, EventArgs e)
        {
            DoneSelectYearEvent?.Invoke(sender, Utility.GetIntFromObject((sender as Control).Tag));
        }


    }
}
